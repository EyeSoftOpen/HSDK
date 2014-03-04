namespace EyeSoft.Testing.Domain.Helpers.Domain4
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Domain;

	public class Blog : VersionedAggregate
	{
		private IList<Post> postList;

		protected internal Blog(string ownerEmail) : this()
		{
			OwnerEmail = ownerEmail;
		}

		protected Blog()
		{
			PostList = new List<Post>();
		}

		[Required]
		public virtual string OwnerEmail { get; protected set; }

		public virtual ReadOnlyCollection<Post> Posts
		{
			get { return postList.AsReadOnly(); }
		}

		protected virtual IList<Post> PostList
		{
			get
			{
				return postList;
			}
			private set
			{
				postList = value;
			}
		}

		public virtual void AddPost(string title, DateTime created)
		{
			var post =
				new Post
					{
						Id = Guid.NewGuid(),
						Title = title,
						Created = created,
						Blog = this,
						Data = "Data".ToByteArray()
					};

			PostList.Add(post);
		}
	}
}