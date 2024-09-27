using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TracksifyAPI.Dtos.ProjectUpdates;

// Custom Validation Attributes
public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        try
        {
            if (value is null)
            {
                return ValidationResult.Success; // Or handle null as needed
            }

            var date = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
            {
                return new ValidationResult($"Property with the name {_comparisonProperty} not found.");
            }

            var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue is null)
            {
                return ValidationResult.Success; // Or handle null as needed
            }

            if (date <= comparisonValue)
            {
                return new ValidationResult($"{validationContext.DisplayName} must be greater than {_comparisonProperty}.");
            }

            return ValidationResult.Success;
        }
        catch (Exception ex)
        {
            return new ValidationResult($"An error occurred during validation: {ex.Message}");
        }
    }
}


/**
 * EnsureAtLeastOneElementAttribute - Ensures that atleast one element is in list
 * Inherits from ValidationAttribute
 * Overrides the IsValid method in ValidationAttribute
 */
public class EnsureAtLeastOneElementAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            var list = value as List<Guid>;

            if (list == null || list.Count == 0)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
        catch (Exception ex)
        {
            return new ValidationResult($"An error occurred during validation: {ex.Message}");
        }
    }
}
/**
 * StartDateNotInPastAttribute - Ensures that start date is not in the past
 * Inherits from ValidationAttribute
 * Overrides the IsValid method in ValidationAttribute
 */
public class StartDateNotInPastAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            var startDate = (DateTime)value;

            if (startDate < DateTime.Today)
            {
                return new ValidationResult("StartDate cannot be in the past.");
            }

            return ValidationResult.Success;
        }
        catch (Exception ex)
        {
            return new ValidationResult($"An error occurred during validation: {ex.Message}");
        }
    }
}

/*public class CheckInNotInFutureAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            var checkIn = (DateTime)value;

            var currentTime = DateTime.Now;
            var today = DateTime.Today;

            // Combine the time portion of CheckIn with the date portion of today
            var checkInDateTime = today.Add(checkIn.TimeOfDay);

            if (checkInDateTime > currentTime)
            {
                return new ValidationResult("CheckIn time cannot be in the future.");
            }

            return ValidationResult.Success;
        }
        catch (Exception ex)
        {
            return new ValidationResult($"An error occurred during validation: {ex.Message}");
        }
    }
}

public class CheckOutNotInFutureAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            var checkOut = (DateTime)value;

            var currentTime = DateTime.Now;
            var today = DateTime.Today;

            // Combine the time portion of CheckOut with the date portion of today
            var checkOutDateTime = today.Add(checkOut.TimeOfDay);

            if (checkOutDateTime > currentTime)
            {
                return new ValidationResult("CheckOut time cannot be in the future.");
            }

            return ValidationResult.Success;
        }
        catch (Exception ex)
        {
            return new ValidationResult($"An error occurred during validation: {ex.Message}");
        }
    }
}


public class CheckOutAfterCheckInAttribute : ValidationAttribute
{
    private readonly string _checkInPropertyName;

    public CheckOutAfterCheckInAttribute(string checkInPropertyName)
    {
        _checkInPropertyName = checkInPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var checkOut = (DateTime)value;

        var checkInProperty = validationContext.ObjectType.GetProperty(_checkInPropertyName);
        if (checkInProperty == null)
        {
            throw new ArgumentException($"Property with the name {_checkInPropertyName} not found.");
        }

        var checkIn = (DateTime)checkInProperty.GetValue(validationContext.ObjectInstance);

        // Combine the time portion of CheckIn with the date portion of today
        var checkInDateTime = DateTime.Today.Add(checkIn.TimeOfDay);

        if (checkOut <= checkInDateTime)
        {
            return new ValidationResult(ErrorMessage ?? "CheckOut time must be after CheckIn time.");
        }

        return ValidationResult.Success;
    }
}*/

/**
 * CheckInCheckOutValidation - Checks that CheckOut is not earlier than CheckIn
 * Inherits from ValidationAttribute
 * Overrides the IsValid method in ValidationAttribute
 */
public class CheckInCheckOutValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var model = (CreateProjectUpdateDto)validationContext.ObjectInstance;
        if (model.CheckIn.TimeOfDay > model.CheckOut.TimeOfDay)
        {
            return new ValidationResult("CheckIn time cannot be greater than CheckOut time.");
        }

        return ValidationResult.Success;
    }
}

