using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace AcademyFramework05._02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(Academy0502Context db = new Academy0502Context())
            {
                var task1 = from tea in db.Teachers
                                        join lec in db.Lectures on tea.Id equals lec.TeacherId
                                        join gl in db.GroupsLectures on lec.Id equals gl.LectureId
                                        join g in db.Groups on gl.GroupId equals g.Id
                                        select new { tea.Name, tea.Surname, GroupName = g.Name };

                var result1 = task1.Distinct().OrderBy(x => x.GroupName).ThenBy(x => x.Name);
                foreach (var item in result1)
                {
                    Console.WriteLine($"Группа: {item.GroupName}, Преподаватель: {item.Name}");
                }

                var task2 = from fac in db.Faculties
                            join dep in db.Departments on fac.Id equals dep.FacultyId
                            where fac.Financing > dep.Financing
                            select fac.Name;

                var result2 = task2.ToList();
                foreach (var item in result2)
                {
                    Console.WriteLine(item);
                }

                var task3 = from cur in db.Curators
                            join gc in db.GroupsCurators on cur.Id equals gc.CuratorId
                            join g in db.Groups on gc.GroupId equals g.Id
                            select new
                            {
                                Curator = cur.Surname,
                                Group = g.Name,
                            };
                var result3 = task3.Distinct().OrderBy(x => x.Curator).ThenBy(x=> x.Group);
                foreach(var item in result3)
                {
                    Console.WriteLine($"Преподаватель: {item.Curator}, Группа: {item.Group}");
                }

                
                var task4 = task1.Where(x => x.GroupName == "ФИЗ-303").ToList();
                foreach (var item in task4)
                {
                    Console.WriteLine($"Группа: {item.GroupName}, Преподаватель: {item.Name} - {item.Surname}");
                }

                var task5 = from tea in db.Teachers
                            join lec in db.Lectures on tea.Id equals lec.TeacherId
                            join gl in db.GroupsLectures on lec.Id equals gl.LectureId
                            join g in db.Groups on gl.GroupId equals g.Id
                            join dep in db.Departments on g.DepartmentId equals dep.Id
                            join fac in db.Faculties on dep.FacultyId equals fac.Id
                            select new
                            {
                                tea.Surname, fac.Name
                            };
                var result5 = task5.ToList();
                foreach (var item in result5)
                {
                    Console.WriteLine($" {item.Surname} - {item.Name}");
                }

                var task6 = from g in db.Groups
                            join dep in db.Departments on g.DepartmentId equals dep.Id
                            select dep.Name + g.Name;
                foreach(var item in task6)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("\n -------- -------");
                //Console.WriteLine(string.Join(", ", db.Teachers.Select(x => x.Name).ToList()));
                var task7 = from sub in db.Subjects
                            join lec in db.Lectures on sub.Id equals lec.SubjectId
                            join tea in db.Teachers on lec.TeacherId equals tea.Id
                            where tea.Name == "Сергей"
                            select sub.Name;
                foreach(var item in task7)
                {
                    Console.WriteLine(item);
                }
                var task8 = from lec in db.Lectures
                            join gl in db.GroupsLectures on lec.Id equals gl.LectureId
                            join g in db.Groups on gl.GroupId equals g.Id
                            join dep in db.Departments on g.DepartmentId equals dep.Id
                            select dep.Name;

                foreach (var item in task8)
                {
                    Console.WriteLine(item);
                }
                var task9 = from g in db.Groups
                            join dep in db.Departments on g.DepartmentId equals dep.Id
                            where dep.Faculty.Name == "Computer Science"
                            select g.Name;

                foreach (var item in task9)
                {
                    Console.WriteLine(item);
                }
                var task10 = from g in db.Groups
                             where g.Year == 5
                             join dep in db.Departments on g.DepartmentId equals dep.Id
                             select new { GroupName = g.Name, FacultyName = dep.Faculty.Name };

                foreach (var item in task10)
                {
                    Console.WriteLine($"{item.GroupName} - {item.FacultyName}");
                }
                var task11 = from tea in db.Teachers
                             join lec in db.Lectures on tea.Id equals lec.TeacherId
                             join gl in db.GroupsLectures on lec.Id equals gl.LectureId
                             join g in db.Groups on gl.GroupId equals g.Id
                             where lec.LectureRoom == "B103"
                             select new { TeacherFullName = tea.Name + " " + tea.Surname, GroupName = g.Name };

                foreach (var item in task11)
                {
                    Console.WriteLine($"{item.TeacherFullName}: {item.GroupName}");
                }
            }   
        }
    }
}
