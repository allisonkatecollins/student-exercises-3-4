using System;
using StudentExercises3_4.Data;
using StudentExercises3_4.Model;
using System.Collections.Generic;

namespace StudentExercises3_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //create an instance of the Repository class in order to use its methods
            //to interact with the database
            Repository repository = new Repository();

            //call GetAllExercises from Repository.cs
            List<Exercise> exercises = repository.GetAllExercises();

            Console.WriteLine("All Exercises:");

            foreach (Exercise exercise in exercises)
            {
                Console.WriteLine(exercise.Name, exercise.Language);
            }

            Pause();

            //call GetJavaScriptExercises from Repository.cs

            //add new exercise

            //call GetInstructorsWithCohort from Repository.cs
            //include each instructor's cohort

            //add new instructor and assign to existing cohort

            //assign existing exercise to existing student

        }
        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key...");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
