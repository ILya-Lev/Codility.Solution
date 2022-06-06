using System.Collections.Generic;

namespace UjvalsProposal.Models;

public class Course : NotifyPropertyChanged
{
    private string? _name;
    private List<Student> _students;

    public string? Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    //make readonly, expose readonly list interface, extend with Add adn AddRange methods
    public List<Student> Students
    {
        get => _students;
        set
        {
            _students = value;
            OnPropertyChanged();
        }
    }
}