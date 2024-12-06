using LibGit2Sharp;
using LitHub.Contract;
using System;
using System.Collections.Generic;
using System.IO;

namespace LitHub.Services
{
    public class GitService : IGitService
    {
        private const string basePath = "F:\\Projects\\Lithub\\Server\\testrepo";

        public GitService(IServiceProvider serviceProvider)
        {

        }

        public Hub GetHub(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hub> GetHubs()
        {
            throw new NotImplementedException();
        }

        public string GitClone(string url, string localPath)
        {
            return Repository.Clone(url, localPath);
        }

        public void GitCreate(Hub value)
        {
            var repoPath = Path.Combine(basePath, value.Path);
            Repository repository = new Repository(repoPath);
            foreach (var file in value.Books)
            {
                var filePath = Path.Combine(repoPath, file.Path);
                using (var reader = file.FormFile.OpenReadStream())
                {
                    using var writer = System.IO.File.Create(filePath);
                    reader.CopyTo(writer);
                }
                repository.Worktrees.Add(file.Name, filePath, false);
            }
        }

        public void GitDelete(int id)
        {
            throw new NotImplementedException();
        }

        public void GitUpdate(Hub value)
        {
            throw new NotImplementedException();
        }
    }
}
