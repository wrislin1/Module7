/*
 *  Widler Rislin
 11/28/2019
 CEN 4370C
 Module 7 Assignment
 This program is a simple Library check in, check out system where Librarians can see books 
 availble to be checked out, check books out to students check books back in, check students in and out,
 using a checkout interface, only users that are checked in can check out a book. Librarians can also register students
 and add new books. The program has 20 students pre registered and 20 books added.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module7
{
    interface ICheckout  // Checkout interface
    {
       
        void CheckIn();
        void CheckOut();

    }
    class Book: ICheckout  //Book class
    {
        public string title, author;
        public int count = 4;
       public bool isin = true;

        public Book(string title="",string author="")  //Book class constructor
        {
            this.title = title;
            this.author = author;
            
        }

        public void CheckIn()  // Book class version of check in interface
        {
            if (count==4)
            {
                Console.WriteLine("All copies of "+ title +" are checked in");
            }
            else
            {
                isin = true;
                ++count;
            }
        }

        public void CheckOut() //Book class version of checkout interface
        {
            if (count == 0)
                Console.WriteLine("All copies of " + title + " have been checked out");
            else
                --count;
            if (count == 0)
                isin = false;
        }




    }

    class Student: ICheckout  // Student Class
    {
        public string fisrtname, lastname,id;
       public bool isin = false;

        public Student(string firstname="", string lastname="",string id ="")  // Student constructor
        {
            this.fisrtname = firstname;
            this.lastname = lastname;
            this.id = id;
        }

        public void CheckIn() // Student Class version of Checkout interface;
        {
            if (isin)
            {
                Console.WriteLine("Student is already Checked In");
            }
            else isin = true;
        }

        public void CheckOut()  // Student class version of check out interface
        {
            if (isin)
                isin = false;
            else
                Console.WriteLine("Student is already checked out");
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
     
            
            int n=0;
            int nb = 0;
            bool match = false;
            string s="";
            Book[] books = PopulateLibrary(); // Creates array of pre added books for library;
            Student[] students = PopulateStudents(); // creates array of pre registered students for library
            int choice = 0;
            while (choice != 9)  // Menu control loop
            {
                choice = 0;
                Console.WriteLine();
                Console.WriteLine("Welcome to The Library.");    // Selection Menu
                Console.WriteLine("1. Student Check in");
                Console.WriteLine("2. Student Check Out");
                Console.WriteLine("3. List of Registered Students");
                Console.WriteLine("4. List of available Books");
                Console.WriteLine("5. Check Out Book");
                Console.WriteLine("6. Check in Book");
                Console.WriteLine("7. Register Student");
                Console.WriteLine("8. Add Book to Library");
                Console.WriteLine("9. Exit Program");
                Console.WriteLine();

                try //Checking for invalid inputs
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice < 1 || choice > 9)
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine();
                    Console.Write("Input Not Valid, Please try again: ");    // Prompts user for new input
                    Console.WriteLine();

                }

                switch (choice)  // switch statements corresponding to selection
                {
                    case 1:
                        Console.WriteLine("Enter X to return to the Menu");
                        Console.WriteLine();
                        Console.WriteLine("Student Check In");
                        Console.Write("Please enter your Student ID: ");   // Check in system using student id
                        while (!match)
                        {
                            s = Console.ReadLine();
                            if (s == "x" || s == "X")  // escape character check
                                break;
                            try  //check for valid input
                            {

                                n = SearchStudents(ref students, s);  //uses id value to search for student
                                if (n > students.Length)
                                    throw new Exception();
                                else match = true;
                            }
                            catch
                            {
                                Console.WriteLine();
                                Console.Write("Invalid ID, please try again: ");  // re promt for valid input
                            }
                        }
                        match = false;
                        if (s != "x" && s != "X")
                        {
                            if (students[n].isin)  //check to see if student has already checked in
                            {
                                Console.WriteLine("Student already Checked in, Check in Unsucessful");
                                Console.WriteLine();
                            }
                            else
                            {
                                students[n].CheckIn();  // Checks student in
                                Console.WriteLine();
                                Console.WriteLine(students[n].fisrtname + " " + students[n].lastname + " has Checked in Sucessfully");  //Check in confirmation
                            }
                        }

                        s = "";
                        break;
                    case 2:
                        Console.WriteLine("Student Check Out");
                        Console.Write("Please enter your Student ID: "); //Check out based on user id
                        while (!match)
                        {
                            s = Console.ReadLine();
                            if (s == "x" || s == "X")
                                break;

                            try
                            {
                                n = SearchStudents(ref students, s); // search for student using id
                                if (n > students.Length)
                                    throw new Exception();
                                else match = true;
                            }
                            catch
                            {
                                Console.Write("Invalid ID, please try again: ");
                            }
                        }
                        match = false;
                        if (s != "x" && s != "X")
                        {
                            if (!students[n].isin) // check to see if student is already checked out
                            {
                                Console.WriteLine("Student is not Checked in, Check out Unsucessful");
                            }
                            else
                            {
                                students[n].CheckOut(); // checks student out
                                Console.WriteLine(students[n].fisrtname + " " + students[n].lastname + " has Checked out Sucessfully"); //suceess message
                            }
                        }

                        s = "";
                        break;
                    case 3:
                        Console.WriteLine();
                        int i = 1;
                        Console.WriteLine("Name            ID");
                        Console.WriteLine("--------------------");
                        foreach (Student k in students)
                        {
                            Console.WriteLine((i++) + ". " + k.fisrtname + " " + k.lastname + " " + k.id);  //outputs registered students
                        }
                        break;

                    case 4:
                        Console.WriteLine();
                         i = 1;
                        foreach(Book b in books)
                        {
                            Console.WriteLine((i++) +". " + b.title + " by " + b.author + " Copies available: " + b.count);  // out put books and copies available
                        }
                        break;
                    case 5:
                        Console.Write("Please enter Student ID: "); //id needed to check out

                        while (!match)
                        {
                            s = Console.ReadLine();
                            if (s == "x" || s == "X")
                                break;
                            try
                            {
                                n = SearchStudents(ref students,s); //search for student
                                if (n > students.Length)
                                    throw new Exception();  //either id invalid or no student found
                                else match = true;
                            }
                            catch
                            {
                                Console.WriteLine();
                                Console.Write("Invalid ID, please try again: ");
                            }
                        }
                        match = false;

                        if ((s != "x" && s != "X") && students[n].isin)
                        {
                                                    
                                Console.Write("Please enter the title of the Book, titles are case sensitive: ");
                                while (!match)
                                {
                                    s = Console.ReadLine();
                                    if (s == "x" || s == "X")
                                        break;
                                    try
                                    {
                                        nb = SearchBooks(ref books, s);  // searched for book, exact match needed
                                        if (nb > students.Length)
                                            throw new Exception();
                                        else match = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Invalid Book title, Please try again");
                                        Console.Write("return to menu to see list of available Books: ");
                                        Console.WriteLine();
                                    }
                                }
                            

                            
                                if (s != "x" && s != "X")
                                {
                                    if (!books[nb].isin)  // check to see if copies are available
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("All copies of " + books[nb].title + " have been checked out");
                                        Console.WriteLine("Check Out Unsucessful");
                                        Console.WriteLine();
                                    }

                                    else
                                    {
                                        books[nb].CheckOut();  // heck out sucess
                                        Console.WriteLine();
                                        Console.WriteLine(students[n].fisrtname + " " + students[n].lastname + " has Checked out " + books[nb].title + " Sucessfully");
                                        
                                    }
                                }
                            
                        }
                        else if(!students[n].isin)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Student not Checked in, please check in to check out books");  // checks to see if student is checked in
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                        s = "";
                        match = false;
                        break;
                        case 6:
                        Console.WriteLine();
                        Console.Write("Please enter title of Book: ");
                        while (!match)
                        {
                            s = Console.ReadLine();
                            if (s == "x" || s == "X")
                                break;
                            try
                            {
                                n = SearchBooks(ref books, s);
                                if (n > books.Length)
                                    throw new Exception();
                                else match = true;
                            }
                            catch
                            {
                                Console.WriteLine();
                                Console.Write("Invalid Book title, Please try again: ");
                                Console.WriteLine();

                               
                            }
                            
                           
                        }

                        if (s != "x" && s != "X")
                        {
                            if (books[n].count == 4)
                            {
                                Console.WriteLine();
                                Console.WriteLine("All copies of " + books[n].title + " have been checked in");
                                Console.WriteLine("Check in Unsucessful");
                                Console.WriteLine();
                                
                            }

                            else
                            {
                                books[n].CheckIn();  // checks book in
                                Console.WriteLine();
                                Console.WriteLine(books[n].title + " has been Checked in Sucesfully");  
                                Console.WriteLine();
                            }
                        }
                        match = false;
                        break;
                    case 7:
                        AddStudent(ref students);
                        break;
                    case 8:
                        AddBooks(ref books);
                        break;


                }
                
            }
        }
        static Book[] PopulateLibrary() //Populates library with pre added books
        {
            Book[] books = 
                {
                new Book("In Search of Lost Time","Marcel Proust"),new Book("Ulysses","James Joyce"),new Book("War and Peace","Leo Tolstoy"),new Book("Don Quixote","Miguel de Cervantes"),new Book("The Great Gatsby","F. Scott Fitzgerald"),
                new Book("Moby Dick","Herman Melville"),new Book("One Hundred Years of Solitude","Gabriel Garcia Marquez"),new Book("Jane Eyre","Charlotte Bronte"),new Book("Hamlet","William Shakespeare"),new Book("Lolita","Vladimir Nabokov"),
                new Book("The Odyssey","Homer"),new Book("The Adventures of Huckleberry Finn","Mark Twain"),new Book("The Catcher in the Rye","J. D. Salinger"),new Book("The Divine Comedy","Dante Alighieri"),new Book("Alice's Adventures in Wonderland","Lewis Carroll"),
                new Book("Wuthering Heights","Emily Brontë"),new Book("Pride and Prejudice","Jane Austen"),new Book("The Sound and the Fury","William Faulkner"),new Book("Heart of Darkness","Joseph Conrad"),new Book("Catch-22","Joseph Heller"),


            };
            return books;
        }

        static Student[] PopulateStudents() // pree rwgistered students
        {
            Student[] students =
                {
                new Student("Layton","Barrett","001"),new Student("Bree", "Bloom","002"),new Student("Casper", "Gordon","003"),new Student("Shaun", "Crossley","004"),new Student("Duke", "Peel","005"),
                new Student("Janine", "Powers","006"),new Student("Jeanne", "Milner","007"),new Student("Johnathan", "Hulme","008"),new Student("Adriana", "Zavala","009"),new Student("Harvey-Lee", "Zhang","0010"),
                new Student("Matylda", "Ho","0011"),new Student("Bradleigh", "Bautista","0012"),new Student("Eliot", "Mcdaniel","0013"),new Student("Paulina", "Mcconnell","0014"),new Student("Ferne", "Owens","0015"),
                new Student("Tamera", "Clarkson","0016"),new Student("Harriett", "Yu","0017"),new Student("Nishat", "Winter","0018"),new Student("Meera", "Weir","0019"),new Student("Eilidh", "Perez","0020")

            };
            return students;
        }
       




        static int SearchStudents(ref Student[] students,string id) // searches student based on matching Id
        {
            int n=students.Length+1;
            bool match=false;
           
            for (int i = 0; i < students.Length; ++i)
            {
                if (students[i].id == id)
                {
                    match = true;
                    n = i;

                }
            }
            if (match)
            {

                return n;
            }

            else return students.Length+1;
        }

   

        static int SearchBooks(ref Book[] books,string title) //searches for book based on matching title
        {
            bool match = false;
            int n = books.Length+1;
            for (int i = 0; i < books.Length; ++i)
            {
                if (books[i].title == title)
                {
                    match = true;
                    n = i;
                }

            }
            if (match)
            {
                return n;

            }

            else return (books.Length + 1);

        }
        static void AddStudent(ref Student[] students) // add students, prompts for info
        {

            string f="",l="",id = "00" + (students.Length+1);
            Console.Write("Please enter your First name, only alphbetical characters allowed: ");
            do
            {
                try
                {
                    f = Console.ReadLine();
                    if (f == "1")
                        break;
                    if (!f.All(char.IsLetter) || string.IsNullOrEmpty(f))
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine();
                    Console.Write("Invalid First name, please try again: ");
                }


            }
            while (f != "1" && (!f.All(char.IsLetter) || string.IsNullOrEmpty(f)));



            if (f != "1")
            {

                Console.WriteLine();
                Console.Write("Please enter your last name, only alphbetical characters allowed: ");
                do
                {
                    try
                    {
                        l = Console.ReadLine();
                        if (l == "1")
                            break;
                        if (!l.All(char.IsLetter) || string.IsNullOrEmpty(l))
                            throw new Exception();
                    }

                    catch
                    {
                        Console.WriteLine();
                        Console.Write("Invalid last name, please try again: ");

                    }
                }
                while (l != "1" && (!l.All(char.IsLetter) || string.IsNullOrEmpty(l)));

                if (f != "1" && l != "1")
                {
                    Student[] temp = new Student[students.Length + 1];
                    students.CopyTo(temp, 0);
                    temp[students.Length] = new Student(f, l, id);
                    students = new Student[temp.Length];
                    temp.CopyTo(students, 0);
                    Console.WriteLine();
                    Console.WriteLine(f + " " + l + " has been registered");
                    Console.WriteLine();
                }
            }


            

        }

        static void AddBooks(ref Book[] books)  //add books, prompts for info
        {
            string title = "", author = "";      
            

            Console.WriteLine("Press 1 to return to menu");
            Console.WriteLine();
            Console.Write("Please enter book title, all characters accepted, cannot be blank: ");
            do
            {
                try
                {
                    title = Console.ReadLine();
                    if (title == "1")
                        break;
                    if (string.IsNullOrEmpty(title))
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine();
                    Console.Write("Invalid title, please try again: ");


                }
            }        
            while (string.IsNullOrEmpty(title));

            if (title != "1")
            {
                Console.WriteLine();
                Console.Write("Please enter author, if no author, enter Anonymous: ");
                do
                {
                    try
                    {
                        author = Console.ReadLine();
                        if (author == "1")
                            break;
                        if (string.IsNullOrEmpty(author))
                            throw new Exception();
                    }

                    catch
                    {
                        Console.WriteLine();
                        Console.Write("Invalid Author, please try again: ");

                    }
                }

                while (author != "1" && string.IsNullOrEmpty(author));
            }

            if(title != "1" && author != "1")
            {
                Book[] temp = new Book[books.Length + 1];
                books.CopyTo(temp, 0);
                temp[books.Length] = new Book(title, author);
                books = new Book[temp.Length];
                temp.CopyTo(books, 0);
                Console.WriteLine();
                Console.WriteLine(title + " by " + " " + author + " has been added to the Library");
                Console.WriteLine();
            }



        }
    }
}
