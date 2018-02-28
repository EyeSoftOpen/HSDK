param($installPath, $toolsPath, $package, $project)

#split-path -parent twice to get the right dir name
$projDir = Split-Path -Parent $project.FullName
#generate our xmla
$projName = ""
if ( $project.ProjectName -ne $null -and $project.ProjectName -ne "" ) {
    $projName = $project.ProjectName
}
else {
    $projName = $project.Name
}
$params = "`"$projDir`" `"$projName`""
Start-Process "$toolsPath\NugetInstaller.exe" $params -Wait

$installFile = "InstallLeanSentryAzure.exe"
$installConfig = "InstallLeanSentryAzure.exe.config"
$configFile = "leansentry.config"
$prepServerFile = "prepserver_azure.bat"

#make sure we have the config file with a length > 20...
#if it's smaller it means it didnt get written and we abort
if ((Get-ChildItem $projDir | Where-Object { $_.Name -eq $configFile }).Length -gt 20) {
    # pulled from stack overflow
    # Modify the service config - adding a new Startup task
    # do it for each project that it's applicable to
    $roleType = ""
    $DTE.Solution.Projects | ForEach {
        $svcConfigFile = $_.ProjectItems | Where-Object{$_.Name -eq 'ServiceDefinition.csdef'}
        if ( $svcConfigFile -ne $null )
        {
            $ServiceDefinitionConfig = $svcConfigFile.Properties.Item("FullPath").Value
            [xml] $xml = Get-Content $ServiceDefinitionConfig
            
            #create a dynamic array to hold every thing
            $allRoles = @()
            $validProject = $false
            foreach( $ele in $xml.ServiceDefinition.WebRole )
            {
                #make sure the projects match
                if ( $ele.Name -ne $null -and $ele.Name -eq $project.Name )
                {
                    $allRoles += $ele
                    $validProject = $true
                    $roleType = "web"
                }
            }
            foreach( $ele in $xml.ServiceDefinition.WorkerRole )
            {
                if ( $ele.Name -ne $null -and $ele.Name -eq $project.Name )
                {
                    $allRoles += $ele
                    $validProject = $true
                    $roleType = "worker"
                }
            }
            
            # Check to see if the startup node exists
            foreach ( $ele in $allRoles )
            {
                # So that you dont get the blank ns in your node
                $startupNode = $xml.CreateElement('Startup','http://schemas.microsoft.com/ServiceHosting/2008/10/ServiceDefinition')
                $prepServer = $xml.CreateElement('Task','http://schemas.microsoft.com/ServiceHosting/2008/10/ServiceDefinition')
                $prepServer.SetAttribute('commandLine',"Startup\$prepServerFile")
                $prepServer.SetAttribute('executionContext','elevated')
                $prepServer.SetAttribute('taskType','simple')
                $startupNode.AppendChild($prepServer)

                $installLS = $xml.CreateElement('Task','http://schemas.microsoft.com/ServiceHosting/2008/10/ServiceDefinition')
                if ( $ele.LocalName -eq "WebRole" ) {
                    $installLS.SetAttribute('commandLine',"Startup\$installFile ..\..\$configFile /noerror")
                } elseif ( $ele.LocalName -eq "WorkerRole" ) {
                   $installLS.SetAttribute('commandLine',"Startup\$installFile ..\$configFile /noerror")
                }                
                $installLS.SetAttribute('executionContext','elevated')
                $installLS.SetAttribute('taskType','background')
                $startupNode.AppendChild($installLS)
                
                
                #Create startup and task 
                $startUp = $ele.Startup
                if($startUp -eq $null){
                    $ele.PrependChild($startupNode)
                    $startUp = $ele.Startup
                }
                elseif($startUp -eq "" -and $startUp.GetType().Name -eq "String") {
                    #cut the element out because it thinks it's a string. it isn't.
                    $n = $ele.InnerXML
                    #get everything up until <startup
                    $head = $n.substring(0, $n.IndexOf("<Startup"))
                    #get everything after the end tag
                    $tail = $n.substring($n.IndexOf("</Startup>") + "</Startup>".Length)
                    #update the xml
                    $ele.InnerXML = $head+$tail
                    #now add the startup node
                    $ele.PrependChild($startUpNode)
                }
                else{
                    $prepServerExists = $false
                    $installLSExists = $false
                    foreach ($i in $ele.Startup.Task){
                        if ($i.commandLine -eq "Startup\$prepServerFile"){
                            $prepServerExists = $true
                        }
                        if ( $i.commandLine -eq "Startup\$installFile ..\..\$configFile /noerror") {
                            $installLSExists = $true
                        }
                    }
                    # do the same with LS install
                    if($installLS -ne $null -and !$installLSExists){
                        $startUp.AppendChild($installLS)
                    }
                    # so if prepserver xml exists but it's not added...
                    if($prepServer -ne $null -and !$prepServerExists){
                        $startUp.AppendChild($prepServer)
                    }
                }
            }
            $xml.Save($ServiceDefinitionConfig);
        }
    }


    #finally, set attributes on all items that always need to be copied
    foreach ( $i in $project.ProjectItems ) {
    	if ( $i.Name -eq "Startup" ) {
            foreach ( $j in $i.ProjectItems ) {
                if ( $j.Name -eq $prepServerFile -or $j.Name -eq $installFile -or $j.Name -eq $installConfig) {
            		foreach ( $p in $j.Properties ) {
            			if ( $p.Name -eq "CopyToOutputDirectory" ) {
            				$p.Value = 1
            			}
                        if ( $p.Name -eq "BuildAction" ) {
                            $p.Value = 2
                        }
            		}
            	}
            }
        }
    	if ( $i.Name -eq $configFile ) {
    		foreach ( $p in $i.Properties ) {
    			if ( $p.Name -eq "CopyToOutputDirectory" ) {
    				$p.Value = 1
    			}
                if ( $p.Name -eq "BuildAction" ) {
                    if ( $roleType -eq "web" ) {
                        $p.Value = 2
                    }
                    else {
                        $p.Value = 0
                    }
                }
    		}
    	}
    }
    
    #make sure to save the project file
    $project.Save($project.FullName)
}
else {
    uninstall-package LeanSentryAzureDeployment -ProjectName $projName
}