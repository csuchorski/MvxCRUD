using Dapper;
using MvxStarter.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvxStarter.Core.DataAccess
{
    public static class PersonDA
    {
        public static List<PersonModel> GetListOfPeople(string inName)
        {
            using IDbConnection conn = new SqlConnection(Helper.CnnVal("PeopleDB"));
            return conn.Query<PersonModel>("dbo.PersonSearch @Name", new { Name = inName }).ToList();
        }
        public static void AddPersonToDB(PersonModel p)
        {
            using IDbConnection conn = new SqlConnection(Helper.CnnVal("PeopleDB"));
            conn.Execute("dbo.PersonAdd", new { Name = p.Name, Country = p.Country, Email = p.Email }, commandType: CommandType.StoredProcedure);
        }
        public static void DeletePersonFromDB(PersonModel p)
        {
            using IDbConnection conn = new SqlConnection(Helper.CnnVal("PeopleDB"));
            conn.Execute("dbo.PersonDelete", new { PersonID = p.PersonID }, commandType: CommandType.StoredProcedure);
        }
        public static void UpdatePersonFromDB(PersonModel p)
        {
            using IDbConnection conn = new SqlConnection(Helper.CnnVal("PeopleDB"));
            conn.Execute("dbo.PersonUpdate", new { PersonID = p.PersonID, Name = p.Name, Country = p.Country, Email = p.Email }, commandType: CommandType.StoredProcedure);
        }
        public static PersonModel FindLastPerson()
        {
            using IDbConnection conn = new SqlConnection(Helper.CnnVal("PeopleDB"));
            return conn.Query<PersonModel>("dbo.FindLastPerson").Last();      
        }
    }
}
           

