namespace Domain.DTOs;

public class UserCurrentReservationDto
{
    public string SpaceName { get; set; }
    public DateOnly SubmitDate { get; set; }
    public int DaysNumber { get; set; }
}