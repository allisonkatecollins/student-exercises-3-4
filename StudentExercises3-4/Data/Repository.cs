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
                string _connectionString = "Server=ALLISONCOLLINS-\\SQLEXPRESS; Database=StudentExercises3; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                return new SqlConnection(_connectionString);
            }
        }
        //Query the database for all the Exercises.
        public List<Exercise> GetAllExercises()
        { 
            //must "use" the database connection
            //Open() connections when we need to interact with the database
            //Close() connections when we're finished
            //a "using" block ensures we correctly disconnect from a resource even if there's an error
            using (SqlConnection conn = Connection)
            {
                //open the connection
                conn.Open();

                //we have to "use" commands too
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //set up the command with the SQL we want to execute before we execute it
                    cmd.CommandText = "SELECT Id, ExerciseName, ExerciseLanguage FROM Exercise";

                    //execute the SQL in the database and get a "reader" that will give us access to the data
                    SqlDataReader reader = cmd.ExecuteReader();

                    //a list to hold the exercises we retrieve from the database
                    List<Exercise> exercises = new List<Exercise>();

                    //Read() will return true if there's more data to read
                    while (reader.Read())
                    {
                        //the "ordinal" is the numeric position of the column in the query results
                        int idColumnPosition = reader.GetOrdinal("Id");
                        //we use the reader's GetXXX methods to get the value for a particular ordinal
                        int idValue = reader.GetInt32(idColumnPosition);

                        int ExerciseNamePosition = reader.GetOrdinal("Name");
                        string ExerciseValue = reader.GetString(ExerciseNamePosition);

                        int LanguagePosition = reader.GetOrdinal("Language");
                        string LanguageValue = reader.GetString(LanguagePosition);

                        //create a new exercise object using the data from the database
                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            ExerciseName = ExerciseValue,
                            ExerciseLanguage = LanguageValue
                        };
                        
                        exercises.Add(exercise);
                    }
                    reader.Close();
                    //return the list of exercises to whomever called this method
                    return exercises;
                }
            }
        }

        //Find all the exercises in the database where the language is JavaScript.
        public List<Exercise> GetJSExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, ExerciseName, ExerciseLanguage FROM Exercise where ExerciseLanguage = 'JavaScript'";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Exercise> JSExercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        int IdPosition = reader.GetOrdinal("Id");
                        int IdValue = reader.GetInt32(IdPosition);

                        int ExerciseNamePosition = reader.GetOrdinal("Name");
                        string ExerciseValue = reader.GetString(ExerciseNamePosition);

                        int LanguagePosition = reader.GetOrdinal("Language");
                        string LanguageValue = reader.GetString(LanguagePosition);

                        //create new instance
                        Exercise JSExercise = new Exercise
                        {
                            Id = IdValue,
                            ExerciseName = ExerciseValue,
                            ExerciseLanguage = LanguageValue
                        };

                        JSExercises.Add(JSExercise);
                    }

                    reader.Close();
                    return JSExercises;
                }
            }
        }
        //Insert a new exercise into the database.
        public void AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = 
                        $@"INSERT INTO Exercise(ExerciseName,ExerciseLanguage) Values(@Name, @Language)";

                    cmd.Parameters.Add(new SqlParameter("@Name", exercise.ExerciseName));

                    cmd.Parameters.Add(new SqlParameter("@Language", exercise.ExerciseLanguage));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Find all instructors in the database. Include each instructor's cohort.
        public List<Instructor> GetInstructorsWithCohort()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                        cmd.CommandText = 
                            @"SELECT Instructor.Id, Instructor.FirstName, Instructor.LastName, Cohort.Id as CohortId, 
                            Cohort.CohortName as CohortName FROM Instructor
                            LEFT JOIN Cohort ON Instructor.CohortId = Cohort.Id";
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<Instructor> Instructors = new List<Instructor>();
                        while (reader.Read())
                        {
                            int InstructorIdPosition = reader.GetOrdinal("Id");
                            int InstructorIdValue = reader.GetInt32(InstructorIdPosition);

                            int InstructorFirstNamePosition = reader.GetOrdinal("FirstName");
                            string InstructorFirstNameValue = reader.GetString(InstructorFirstNamePosition);

                            int InstructorLastNamePosition = reader.GetOrdinal("LastName");
                            string InstructorLastNameValue = reader.GetString(InstructorLastNamePosition);

                            int CohortIdPosition = reader.GetOrdinal("CohortId");
                            int CohortIdValue = reader.GetInt32(CohortIdPosition);

                            int CohortNamePosition = reader.GetOrdinal("CohortName");
                            string CohortNameValue = reader.GetString(CohortNamePosition);

                            Cohort cohort = new Cohort
                            {
                                Id = CohortIdValue,
                                Name = CohortNameValue
                            };

                            Instructor instructor = new Instructor
                            {
                                Id = InstructorIdValue,
                                FirstName = InstructorFirstNameValue,
                                LastName = InstructorLastNameValue,
                                CohortId = CohortIdValue,
                                cohort = cohort
                            };

                            Instructors.Add(instructor);
                        }
                        reader.Close();
                        return Instructors;
                }
            }
        }
        //Insert a new instructor into the database.Assign the instructor to an existing cohort.
        public void AddInstructor(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = 
                        $@"INSERT INTO Instructor(FirstName,LastName,SlackHandle,CohortId) Values(@FirstName, @LastName, @SlackHandle, @CohortId)";

                    cmd.Parameters.Add(new SqlParameter("@FirstName", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@SlackHandle", instructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@CohortId", instructor.CohortId));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Assign an existing exercise to an existing student.
        public void AddStudentExercise(int studentId, int exerciseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = 
                        $@"INSERT INTO StudentExercise(StudentId, ExerciseId) Values(@studentId, @exerciseId)";
                    cmd.Parameters.Add(new SqlParameter("@studentId", studentId));
                    cmd.Parameters.Add(new SqlParameter("@exerciseId", exerciseId));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<StudentExercise> GetStudentExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = 
                        @"SELECT se.Id as StudentExerciseId, StudentId, ExerciseId, s.FirstName, s.LastName, 
                        s.SlackHandle, s.CohortId, e.Name as ExerciseName, e.Language FROM StudentExercise as se 
                        LEFT JOIN Student as s ON se.StudentId = s.Id 
                        JOIN Exercise as e  ON se.ExerciseId = e.Id;";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<StudentExercise> StudentExercises = new List<StudentExercise>();
                    while (reader.Read())
                    {
                        int StudentExerciseIdPosition = reader.GetOrdinal("StudentExerciseId");
                        int StudentExerciseIdValue = reader.GetInt32(StudentExerciseIdPosition);

                        int StudentIdPosition = reader.GetOrdinal("StudentId");
                        int StudentIdValue = reader.GetInt32(StudentIdPosition);

                        int ExerciseIdPosition = reader.GetOrdinal("ExerciseId");
                        int ExerciseIdValue = reader.GetInt32(ExerciseIdPosition);

                        int StudentFirstNamePosition = reader.GetOrdinal("FirstName");
                        string StudentFirstNameValue = reader.GetString(StudentFirstNamePosition);

                        int StudentLastNamePosition = reader.GetOrdinal("LastName");
                        string StudentLastNameValue = reader.GetString(StudentLastNamePosition);

                        int SlackHandlePosition = reader.GetOrdinal("SlackHandle");
                        string SlackHandleValue = reader.GetString(SlackHandlePosition);

                        int CohortIdPosition = reader.GetOrdinal("CohortId");
                        int CohortIdValue = reader.GetInt32(CohortIdPosition);

                        int ExerciseNamePosition = reader.GetOrdinal("ExerciseName");
                        string ExerciseNameValue = reader.GetString(ExerciseNamePosition);

                        int ExerciseLanguagePosition = reader.GetOrdinal("Language");
                        string ExerciseLanguageValue = reader.GetString(ExerciseLanguagePosition);

                        Exercise exercise = new Exercise
                        {
                            Id = ExerciseIdValue,
                            ExerciseName = ExerciseNameValue,
                            ExerciseLanguage = ExerciseLanguageValue
                        };

                        Student student = new Student
                        {

                        }
                    }
                }
            }
        }
    }
}
