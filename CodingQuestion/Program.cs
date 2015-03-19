using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingQuestion {
	class Program {
		static void Main( string[] args ) {

			int[] input = { 75, 75, 75, 75, 99, 50 };


			foreach( int i in GetRanksNaive( input ) ) {
				Console.Write( i );
				Console.Write( ',' );
			}
			Console.ReadLine();
		}

		static int[] GetRanks( int[] grades ) {

			List<int>[] gradeBuckets = new List<int>[101];

			for( int i = 0; i < grades.Length; i++ ) {
				if( gradeBuckets[grades[i]] == null ) {
					gradeBuckets[grades[i]] = new List<int>();
				}
				gradeBuckets[grades[i]].Add( i );
			}

			int[] returnValue = new int[grades.Length];
			int rank = 1;
			
			for( int i = 100; i >= 0; i-- ) {
					
				if( gradeBuckets[i] != null ) {
					int tiedRanks = 0;
					foreach( int student in gradeBuckets[i] ) {
						returnValue[student] = rank;
						tiedRanks++;
					}

					rank += tiedRanks == 0?1:tiedRanks;
				}
			}

			return returnValue;
		}

		struct GradeAndIndex {
			public int Grade;
			public int Index;
		}

		static IEnumerable<int> GetRanksNaive( int[] grades ) {

			var gandis = Convert( grades ).OrderByDescending( x => x.Grade );

			int[] returnValue = new int[grades.Length];
			int rank = 0;
			int lastScore = 101;

			foreach( GradeAndIndex sortedGrade in gandis ) {
				if( sortedGrade.Grade != lastScore ) {
					rank++;
				}
				returnValue[sortedGrade.Index] = rank;
				lastScore = sortedGrade.Grade;
				
			}
			return returnValue;
		}

		private static IEnumerable<GradeAndIndex> Convert( int[] grades ) {
			for( int index = 0; index < grades.Length; index++ ) {
				var g = new GradeAndIndex() {
					Grade = grades[ index ],
					Index = index
				};
				yield return g;
			}
		}

	}
}
