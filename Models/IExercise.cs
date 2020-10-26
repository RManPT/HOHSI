using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models
{
    public interface IExercise
    {
        /// <summary>
        /// Gets an exercise data by 'Id'.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Exercise Get(int Id);
        /// <summary>
        /// Gets all Exercises.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Exercise> GetAll();
        /// <summary>
        /// Adds a new exercise to DBContext
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        Exercise Add(Exercise exercise);
        /// <summary>
        /// Updates an existing exercise
        /// </summary>
        /// <param name="exerciseChanges"></param>
        /// <returns></returns>
        Exercise Update(Exercise exerciseChanges);
        /// <summary>
        /// Deletes an exercise.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Exercise Delete(int id);
    }
}
