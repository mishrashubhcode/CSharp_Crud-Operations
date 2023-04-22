using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSharp_Crud_Operations.Models;
using MySql.Data.MySqlClient;


namespace CSharp_Crud_Operations.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeacher</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower('%" + SearchKey + "%') or lower(teacherlname) like lower('%" + SearchKey + "%') or lower(concat(teacherfname, '', teacherlname)) like lower('%" + SearchKey + "%')";
            //cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower('%@key%') or lower(teacherlname) like lower('%@key%') or lower(concat(teacherfname, '', teacherlname)) like lower('%@key%')";

            //cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            //cmd.Prepare();



            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                Decimal TeacherSalary = (Decimal)ResultSet["salary"];
                string TeacherEmployeeNo = (string)ResultSet["employeenumber"];
                DateTime TeacherHireDate = (DateTime)ResultSet["hiredate"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLName = (string)ResultSet["teacherlname"];


                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherEmployeeNumber = TeacherEmployeeNo;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLName;
                
                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a Teacher in the system
        /// </summary>
        /// <example>GET api/TeacherData/FindTeacher</example>
        /// <returns>
        /// A single teacher from the list of teachers
        /// </returns>
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = " +id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                Decimal TeacherSalary = (Decimal)ResultSet["salary"];
                string TeacherEmployeeNo = (string)ResultSet["employeenumber"];
                DateTime TeacherHireDate = (DateTime)ResultSet["hiredate"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLName = (string)ResultSet["teacherlname"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherEmployeeNumber = TeacherEmployeeNo;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLName;

            }

            return NewTeacher;

        }

        /// <summary>
        /// this is used to delete a teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST: /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            // Establish a new command(query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Add a new teacher in the system/database of school
        /// </summary>
        /// <param name="NewTeacher"></param>
        /// <example>
        /// POST api/TeacherData/AddTeacher
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Shubham",
        ///	"TeacherLname":"Mishra",
        ///	"TeacherEmployeeNumber":"T220",
        ///	"TeacherSalary":"90.00"
        /// }
        /// </example>
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            // Establish a new command(query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into Teachers(TeacherFname,TeacherLname,HireDate,EmployeeNumber,Salary) values(@TeacherFname,@TeacherLname,@TeacherHireDate,@TeacherEmployeeNumber,@TeacherSalary)";

            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherHireDate", NewTeacher.TeacherHireDate);
            cmd.Parameters.AddWithValue("@TeacherEmployeeNumber", NewTeacher.TeacherEmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherSalary", NewTeacher.TeacherSalary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
        /// <summary>
        /// Updates a teacher to the Database.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the teacher's table.</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/13
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Anjali",
        ///	"TeacherLname":"Mahida",
        ///	"TeacherEmployeeNumber":"T123",
        ///	"TeacherSalary":"50.00"
        /// }
        /// </example>

        //[EnableCors(origins: "*", methods: "*", headers: "*")]
        [HttpPost]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname,hiredate=@TeacherHireDate employeenumber=@TeacherEmployeeNumber, salary=@TeacherSalary  where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherHireDate", TeacherInfo.TeacherHireDate);
            cmd.Parameters.AddWithValue("@TeacherEmployeeNumber", TeacherInfo.TeacherEmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherSalary", TeacherInfo.TeacherSalary);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
    }
}
