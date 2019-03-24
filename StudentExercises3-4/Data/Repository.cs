using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using StudentExercises3_4.Model;



namespace StudentExercises3_4.Data
{
    class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Server=ALLISONCOLLINS-\\SQLEXPRESS; Database=StudentExerciseDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                return new SqlConnection(_connectionString);
            }
        }
        //Query the database for all the Exercises.
        //Find all the exercises in the database where the language is JavaScript.
        //Insert a new exercise into the database.
        //Find all instructors in the database. Include each instructor's cohort.
        //Insert a new instructor into the database.Assign the instructor to an existing cohort.
        //Assign an existing exercise to an existing student.
    }
}
