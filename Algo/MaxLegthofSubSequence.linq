<Query Kind="Program" />

void Main()
{
	var sol = new Solution();
	var array =  new int[] {36, 41, 56, 35, 44, 33, 34, 92, 43, 32, 42,37};
	sol.MaxSubArray(array);
}

public class Solution {
    public void MaxSubArray(int[] nums) {
        int[] subarray = new int[] {};
		Array.Sort(nums);
		int maxstart=0, start=0, maxlength=0, length =1;
       for(int i =1; i< nums.Length; i++){
	   
	   if(nums[i-1]+1 == nums[i]){
	   
	   length = length+1;}
	   else{
	   start = start+1;
	   length =0;
    }
	
	if(length>=maxlength){
	
	maxstart=start;
	maxlength = length;
	Console.WriteLine("max   "+maxstart+"  "+maxlength);
	}
}
for(int i =0; i< maxlength;i++){
Console.WriteLine(nums[maxstart + i]);
}
}
}
// Define other methods and classes here
