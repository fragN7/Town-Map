using backend.Model;

namespace backend.Validators;

public class PersonValidator
{
    public string ValidatePerson(Person person)
    {
        var errors = string.Empty;
        errors += ValidateName(person.FullName);
        errors += ValidateNickName(person.NickName);
        errors += ValidateBirthDate(person.BirthDate);
        errors += ValidateGender(person.Gender);
        errors += ValidateStatus(person.Status);
        errors += ValidateAddress(person.Street, person.HouseNumber);
        return errors;
    }

    private string ValidateName(string name)
    {
        return string.Empty;
    }
    
    private string ValidateNickName(string nickName)
    {
        return string.Empty;
    }
    
    private string ValidateBirthDate(string date)
    {
        return string.Empty;
    }
    
    private string ValidateGender(string gender)
    {
        return string.Empty;
    }
    
    private string ValidateStatus(string status)
    {
        return string.Empty;
    }

    private string ValidateAddress(string street, string houseNumber)
    {
        return string.Empty;
    }
}