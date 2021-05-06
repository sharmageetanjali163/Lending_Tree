using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LendingTree.Models
{
    public class SystemRole : RoleProvider
    {
        private LendingContext db = new LendingContext();
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (db.Users.Any(b => b.UserId == username))
            {
                string role = "User";
                return new string[] { role };
            }
            else
            {
                if (db.Agents.Any(b => b.AgentId == username))
                {
                    string role = "Agent";
                    var existagent = db.Agents.Where(b => b.AgentId == username).FirstOrDefault();

                    List<string> list = new List<string>();
                    list.Add(role);

                    if (db.Departments.Any(b => b.DepartmentId == existagent.DepartmentId))
                    {
                        var existdept = db.Departments.Where(b => b.DepartmentId == existagent.DepartmentId).FirstOrDefault();
                        list.Add(existdept.DepartmentName);
                    }

                    return list.ToArray();
                }
                else
                {
                    string role = "Admin";

                    return new string[] { role };
                }
            }
            // throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}