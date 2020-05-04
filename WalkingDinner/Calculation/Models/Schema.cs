using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingDinner.Models;

namespace WalkingDinner.Calculation.Models {

    public class Schema {

        private static Random random = new Random();

        private static void ShuffleArray<T>( ref T[] array ) {

            for ( int i = 0; i < array.Length; i++ ) {

                int newIndex = random.Next( 0, array.Length );

                // Swap
                T temp              = array[ i ];
                array[ i ]          = array[ newIndex ];
                array[ newIndex ]   = temp;
            }
        }

        public static Schema GenerateSchema( Couple[] allCouples, int courseCount ) {

            // ShuffleArray( ref allCouples );
            int coupleCount = allCouples.Length;

            int parallelCount   = coupleCount / courseCount;
            int couplesPerMeal  = courseCount;

            Schema schema = new Schema( courseCount, couplesPerMeal );
            schema.Courses[ 0 ] = Course.SeedCourse( allCouples, parallelCount, couplesPerMeal );

            for ( int i = 1; i<courseCount; i++ ) {

                Course course = Course.CopyFrom( schema.Courses[ i-1 ] );

                for ( int j = 1; j < couplesPerMeal; j++ ) {

                    int shiftcount = j;

                    course.Shift( j, shiftcount );
                }

                schema.Courses[ i ] = course;
            }

            return schema;
        }

        public Course[] Courses { get; }
        public int CouplesPerMeal { get; }

        public Schema( int count, int couplesPerMeal ) {

            CouplesPerMeal  = couplesPerMeal;
            Courses         = new Course[ count ];
        }
    }
}
