<Query Kind="Program" />

 class Solution {
    public int solution(int[] A) {
        // write your code in C# 6.0 with .NET 4.5 (Mono)
       
        int unPleasantCombinationsCount = IsPleasant(A);
        
        if(unPleasantCombinationsCount==0){
            return 0;
        }
        int count = 0;
        for (int i = 0; i< A.Length; i++){

            if(IsPleasant(A.Where((e,index) => i!= index).ToArray()) ==0){
                    count++;
            }
        }
        if(count==0){
            return -1;
        }
    return count;
        
    }

    private int IsPleasant(int[] A){
         int unPleasantCombinationsCount = 0;
        for(int i =1; i< A.Length-1; i = i+1){
            if(A[i-1]> A[i] && A[i]<A[i+1]){
                continue;
            }

            if(A[i-1]< A[i] && A[i]>A[i+1]){
                continue;
            }
            unPleasantCombinationsCount += 1;
        }

        return unPleasantCombinationsCount;
    }
}
	
	void Main()
	{
		var sol = new Solution();
		
		var val = sol.solution(new int[] {3, 4, 3, 7,8,9});
		Console.WriteLine(val);
		
		
	}

// Define other methods and classes here
