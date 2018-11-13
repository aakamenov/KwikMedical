using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Login.Models;
using LiteDB;

namespace Login.Infrastructure
{
    public class UserStore : IUserStore
    {
        private const string DB_NAME = "Users.db";
        private readonly object @lock = new object();

        public Task<bool> AddUser(string username, string password)
        {
            var tcs = new TaskCompletionSource<bool>();
            
            lock (@lock)
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var users = db.GetCollection<User>();

                    var existsQuery = Query.Contains("Username", username);
                    var exists = users.Exists(existsQuery);

                    if (exists)
                    {
                        tcs.SetResult(false);

                        return tcs.Task;
                    }

                    var user = new User(username, password)
                    {
                        Token = Guid.NewGuid().ToString(),
                        TokenExpirationDate = DateTime.Now.AddDays(7)
                    };

                    users.Insert(user);
                }
            }

            tcs.SetResult(true);
            return tcs.Task;
        }

        public Task<bool> AuthenticateUser(string token)
        {
            var tcs = new TaskCompletionSource<bool>();

            lock(@lock)
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var users = db.GetCollection<User>();

                    var query = Query.Contains("Token", token);
                    var user = users.FindOne(query);

                    if (user is null)
                        tcs.SetResult(false);
                    else if (user.TokenExpirationDate < DateTime.Now)
                        tcs.SetException(new Exception("Token expired!"));
                    else
                        tcs.SetResult(true);
                }
            }

            return tcs.Task;
        }

        public Task<AuthenticatedUser> GetUser(string username, string password)
        {
            var tcs = new TaskCompletionSource<AuthenticatedUser>();

            lock (@lock)
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var users = db.GetCollection<User>();
                    var userQuery = Query.Contains("Username", username);
                    var user = users.FindOne(userQuery);

                    if (user is null)
                    {
                        tcs.SetException(new Exception("Invalid username"));

                        return tcs.Task;
                    }
                    else if (user.Password != password)
                    {
                        tcs.SetException(new Exception("Invalid password"));

                        return tcs.Task;
                    }

                    if(user.TokenExpirationDate < DateTime.Now)
                    {
                        user.Token = Guid.NewGuid().ToString();
                        user.TokenExpirationDate = DateTime.Now.AddDays(7);

                        UpdateUserPriv(user);
                    }

                    tcs.SetResult(new AuthenticatedUser(user.Username, user.Token));
                    return tcs.Task;
                }
            }
        }

        public Task<bool> UpdateUser(User user)
        {
            var tcs = new TaskCompletionSource<bool>();

            lock (@lock)
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var users = db.GetCollection<User>();

                    if (users.Update(user))
                        tcs.SetResult(true);
                    else
                        tcs.SetResult(false);
                }
            }

            return tcs.Task;
        }

        private void UpdateUserPriv(User user)
        {
            lock (@lock)
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var users = db.GetCollection<User>();
                    users.Update(user);
                }
            }
        }
    }
}
