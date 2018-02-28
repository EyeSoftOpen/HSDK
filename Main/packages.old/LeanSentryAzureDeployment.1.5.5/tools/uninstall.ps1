param($installPath, $toolsPath, $package, $project)
$installFile = "InstallLeanSentryAzure.exe"
$configFile = "leansentry.config"
$prepServerFile = "prepserver_azure.bat"

#manually remove the config file since nuget won't do it automatically because we modify it
$projDir = Split-Path -Parent $project.FullName
del "$projDir\$configFile"
$project.ProjectItems | Where-Object{$_.Name -eq "$configFile"} | ForEach-Object{$_.Remove()}

#remove our lines from the deployment install
$DTE.Solution.Projects | ForEach {
    $svcConfigFile = $_.ProjectItems | Where-Object{$_.Name -eq 'ServiceDefinition.csdef'}
    if ( $svcConfigFile -ne $null )
    {
        $ServiceDefinitionConfig = $svcConfigFile.Properties.Item("FullPath").Value
        [xml] $xml = Get-Content $ServiceDefinitionConfig
        foreach ( $role in $xml.ServiceDefinition.WebRole ) {
            if ( $role.name -ne $null -and $role.name -eq $project.Name ) {
                if ( $role.Startup -ne $null -and $role.Startup.GetType().Name -ne "String" -and $role.Startup.HasChildNodes) {
                    foreach($task in $role.Startup.Task) {
                        if ( $task.commandLine -gt "Startup\$installFile" -or $task.commandLine -eq "Startup\$prepServerFile" ) {
                            $role.Startup.RemoveChild($task)
                        }
                    }
                    #alright, we removed all our children
                    #remove the startup element if there's nothing else
                    if ( $role.Startup.GetType().Name -eq "String" -and $role.Startup -eq "" ) {
                        #cut the element out because it thinks it's a string. it isn't.
                        $n = $role.InnerXML
                        #get everything up until <startup
                        $head = $n.substring(0, $n.IndexOf("<Startup"))
                        #get everything after the end tag
                        $tail = $n.substring($n.IndexOf("</Startup>") + "</Startup>".Length)
                        #update the xml
                        $role.InnerXML = $head+$tail
                    }
                }
            }
        }
        foreach ( $role in $xml.ServiceDefinition.WorkerRole ) {
            if ( $role.name -ne $null -and $role.name -eq $project.Name ) {
                if ( $role.Startup -ne $null -and $role.Startup.GetType().Name -ne "String" -and $role.Startup.HasChildNodes) {
                    foreach($task in $role.Startup.Task) {
                        if ( $task.commandLine -gt "Startup\$installFile" -or $task.commandLine -eq "Startup\$prepServerFile" ) {
                            $role.Startup.RemoveChild($task)
                        }
                    }
                    #alright, we removed all our children
                    #remove the startup element if there's nothing else
                    if ( $role.Startup.GetType().Name -eq "String" -and $role.Startup -eq "" ) {
                        #cut the element out because it thinks it's a string. it isn't.
                        $n = $role.InnerXML
                        #get everything up until <startup
                        $head = $n.substring(0, $n.IndexOf("<Startup"))
                        #get everything after the end tag
                        $tail = $n.substring($n.IndexOf("</Startup>") + "</Startup>".Length)
                        #update the xml
                        $role.InnerXML = $head+$tail
                    }
                }
            }
        }
        $xml.Save($ServiceDefinitionConfig);
    }
}