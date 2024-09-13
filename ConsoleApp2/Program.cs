using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Person
{
	// Закрытые поля
	private string name;
	private string surname;
	private DateTime birthDate;

	// Конструктор с тремя параметрами
	public Person(string name, string surname, DateTime birthDate)
	{
		this.name = name;
		this.surname = surname;
		this.birthDate = birthDate;
	}

	// Конструктор без параметров (значения по умолчанию)
	public Person()
	{
		this.name = "Иван";
		this.surname = "Иванов";
		this.birthDate = new DateTime(2000, 1, 1); // Дата по умолчанию
	}

	// Свойства для доступа к полям
	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	public string Surname
	{
		get { return surname; }
		set { surname = value; }
	}

	public DateTime BirthDate
	{
		get { return birthDate; }
		set { birthDate = value; }
	}

	// Свойство для получения и изменения года рождения
	public int BirthYear
	{
		get { return birthDate.Year; }
		set { birthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
	}

	// Метод для формирования строки со значениями всех полей
	public string ToFullString()
	{
		return $"Имя: {name}, Фамилия: {surname}, Дата рождения: {birthDate.ToShortDateString()}";
	}

	// Метод для формирования строки с именем и фамилией
	public string ToShortString()
	{
		return $"{name} {surname}";
	}
}

public enum Education
{
	Specialist,
	Bachelor,
	SecondEducation
}

public class Exam
{
	// Автосвойства
	public string Subject { get; set; }
	public int Grade { get; set; }
	public DateTime ExamDate { get; set; }

	// Конструктор с параметрами
	public Exam(string subject, int grade, DateTime examDate)
	{
		Subject = subject;
		Grade = grade;
		ExamDate = examDate;
	}

	// Конструктор без параметров
	public Exam()
	{
		Subject = "Неизвестный предмет";
		Grade = 0;
		ExamDate = DateTime.Now;
	}

	// Метод для формирования строки со значениями всех свойств класса
	public string ToFullString()
	{
		return $"Предмет: {Subject}, Оценка: {Grade}, Дата экзамена: {ExamDate.ToShortDateString()}";
	}
}

public class Student
{
	// Закрытые поля
	private Person studentData;
	private Education educationForm;
	private int groupNumber;
	private Exam[] exams;

	// Конструктор с параметрами
	public Student(Person studentData, Education educationForm, int groupNumber)
	{
		this.studentData = studentData;
		this.educationForm = educationForm;
		this.groupNumber = groupNumber;
		this.exams = new Exam[0]; // Инициализация пустого массива экзаменов
	}

	// Конструктор без параметров
	public Student()
	{
		studentData = new Person();
		educationForm = Education.Bachelor;
		groupNumber = 1;
		exams = new Exam[0]; // Инициализация пустого массива экзаменов
	}

	// Свойства для доступа к полям
	public Person StudentData
	{
		get { return studentData; }
		set { studentData = value; }
	}

	public Education EducationForm
	{
		get { return educationForm; }
		set { educationForm = value; }
	}

	public int GroupNumber
	{
		get { return groupNumber; }
		set { groupNumber = value; }
	}

	public Exam[] Exams
	{
		get { return exams; }
		set { exams = value; }
	}

	// Свойство только для чтения, вычисляющее средний балл
	public double AverageGrade
	{
		get
		{
			if (exams.Length == 0) return 0;
			double total = 0;
			foreach (var exam in exams)
			{
				total += exam.Grade;
			}
			return total / exams.Length;
		}
	}

	// Метод для добавления экзаменов
	public void AddExams(params Exam[] newExams)
	{
		int originalLength = exams.Length;
		Array.Resize(ref exams, originalLength + newExams.Length);
		for (int i = 0; i < newExams.Length; i++)
		{
			exams[originalLength + i] = newExams[i];
		}
	}

	// Метод для формирования строки со всеми полями
	public string ToFullString()
	{
		string result = $"Студент: {studentData.ToFullString()}, Форма обучения: {educationForm}, Номер группы: {groupNumber}\n";
		result += "Экзамены:\n";
		foreach (var exam in exams)
		{
			result += exam.ToFullString() + "\n";
		}
		return result;
	}

	// Метод для формирования строки без списка экзаменов
	public string ToShortString()
	{
		return $"Студент: {studentData.ToShortString()}, Форма обучения: {educationForm}, Номер группы: {groupNumber}, Средний балл: {AverageGrade:F2}";
	}
}

public class Program
{
	public static void Main()
	{
		// Создание объекта Student и вывод краткой информации
		Student student = new Student();
		Console.WriteLine(student.ToShortString());

		// Присвоение значений свойствам и вывод полной информации
		student.StudentData = new Person("Екатерина", "Иванова", new DateTime(2004, 3, 13));
		student.EducationForm = Education.Specialist;
		student.GroupNumber = 12;
		Console.WriteLine(student.ToFullString());

		// Добавление экзаменов и вывод полной информации
		student.AddExams(
				new Exam("Мат. анализ", 5, new DateTime(2024, 6, 15)),
				new Exam("Экономика", 4, new DateTime(2024, 6, 18))
		);
		Console.WriteLine(student.ToFullString());
	}
}

