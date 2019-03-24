﻿using System;
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
                Console.WriteLine(exercise.ExerciseName, exercise.ExerciseLanguage);
            }

            Pause();

            //call GetJavaScriptExercises from Repository.cs

            //add new exercise
            Exercise BackgroundColor = new Exercise
            {
                ExerciseName = "Background Color",
                ExerciseLanguage = "CSS"
            };

            repository.AddExercise(BackgroundColor);

            Console.WriteLine("Exercises after adding new exercise:");

            repository.GetAllExercises();
            foreach (Exercise exercise in exercises)
            {
                Console.WriteLine(exercise.ExerciseName, exercise.ExerciseLanguage);
            };

            Pause();

            //call GetInstructorsWithCohort from Repository.cs
            //include each instructor's cohort
            List<Instructor> InstructorsWithCohort = repository.GetInstructorsWithCohort();

            Console.WriteLine("Instructors with cohort:");
            foreach (Instructor instructor in InstructorsWithCohort)
            {
                Console.WriteLine($"{instructor.FirstName} {instructor.LastName} is in {instructor.cohort.Name}");
            };

            Pause();
            //add new instructor and assign to existing cohort
            Instructor Obama = new Instructor
            {
                FirstName = "Barack",
                LastName = "Obama",
                SlackHandle = "POTUS",
                CohortId = 1
            };

            repository.AddInstructor(Obama);
            repository.GetInstructorsWithCohort();
            foreach (Instructor instructor in InstructorsWithCohort)
            {
                Console.WriteLine($"{instructor.FirstName} {instructor.LastName} is in {instructor.cohort.Name}"); }
            }

            Pause();
            //assign existing exercise to existing student

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
