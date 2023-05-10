<Query Kind="Program" />

void Main()
{
	var sol = new BubbLeSort();
	var array =  new int[] {36, 41, 56, 35, 44, 33, 34, 92, 43, 32, 42,37};
	sol.MaxSubArray(array);
	
	
}

public class BubbLeSort {
    public void MaxSubArray(int[] nums) {
               for(int i =0; i< nums.Length-1; i++){
			   	for(int j =1; j< nums.Length-1; j++){
					if(nums[j-1]>nums[j]){
					Console.WriteLine("inside for");
						var temp = nums[j];
						nums[j] = nums[j-1];
						nums[j-1] = temp;
					}
				}
	   	}
		
		for(int i =0; i< nums.Length-1; i++){
	Console.WriteLine(nums[i]);
	}
	}
}
// Define other methods and classes here
