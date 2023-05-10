using System;

internal class Inventor
{
    private string Name;
    private DateTime Dob;
    private string BirthPlace;

    public Inventor(string Name, DateTime Dob, string BirthPlace)
    {
        this.Name = Name;
        this.Dob = Dob;
        this.BirthPlace = BirthPlace;
    }
}