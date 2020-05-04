using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingDinner.Models;

namespace WalkingDinner.Calculation.Models {

    public class Course {

        public Meal[] Meals { get; set; }
        public int CouplesPerMeal { get; }

        public static Course SeedCourse( Couple[] allCouples, int mealCount, int couplesPerMeal ) {

            Course course = new Course( mealCount, couplesPerMeal );

            int index = 0;
            foreach ( Meal meal in course.Meals ) {

                for ( int i = 0; i < meal.Couples.Length; i++ ) {

                    meal.Couples[ i ] = allCouples[ index ];
                    index++;
                }
            }

            return course;
        }

        public static Course CopyFrom( Course source ) {

            Course course = new Course( source.Meals.Length, source.CouplesPerMeal );
            for ( int i = 0; i < source.Meals.Length; i++ ) {

                Array.Copy( source.Meals[ i ].Couples, course.Meals[ i ].Couples, source.Meals[ i ].Couples.Length );
            }

            return course;
        }

        public void Shift( int whichCouple, int howMuch ) {

            for ( int i = 0; i < howMuch; i++ ) {

                var temp = Meals[0].Couples[whichCouple];

                for ( int j = 0; j < Meals.Length - 1; j++ ) {

                    Meals[ j ].Couples[ whichCouple ] = Meals[ j+1 ].Couples[ whichCouple ];
                }

                Meals[ Meals.Length - 1 ].Couples[ whichCouple ] = temp;
            }
        }

        public Course( int count, int couplesPerMeal ) {

            CouplesPerMeal  = couplesPerMeal;
            Meals           = new Meal[ count ];

            for ( int i = 0; i < Meals.Length; i++ ) {

                Meals[ i ] = new Meal( couplesPerMeal );
            }
        }
    }
}
