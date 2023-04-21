using Assignment15;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data.SqlClient;

string myConnection = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Dentist;Integrated Security = true;";
Dentist dentist = new Dentist();
bool more;
string name;
int telNum, choice;

List<Dentist> allDentists = null;

do
{
    Console.WriteLine("Selet from 0...5");
    Console.WriteLine("1.To get the list of all dentists");
    Console.WriteLine("2.To add a new dentist");
    Console.WriteLine("3.To find a dentist");
    Console.WriteLine("4.To update a dentist's phone number");
    Console.WriteLine("5.To delete a dentist");
    Console.WriteLine("0.To exit the program");
    Console.WriteLine();
    Console.Write("Select an option: ");
    //int option = int.Parse(Console.ReadLine());
    string received = Console.ReadLine();
    while (!Int32.TryParse(received, out choice) || choice < 0 || choice > 5)
    {
        Console.WriteLine("Not accepted, try again");
        received = Console.ReadLine();
    }
    switch (choice)
    {
        case 0:
            Console.WriteLine("Do nothing and exiting from the program");
            Console.WriteLine();
            break;
        default:
            Console.WriteLine("Sorry. You will NEVER be here");
            break;
        case 1:
            Console.WriteLine("You selected the option 1. To get the list of all dentist.");
            using (SqlConnection conn = new SqlConnection(myConnection))
            { 
                conn.Open();
                allDentists = dentist.GetAllDentists(conn);
                foreach (Dentist d in allDentists)
                {
                    Console.WriteLine("Id: " + d.Id + ", Name: " + d.Name + ", TelNum: " + d.TelNum);
                }
            }
            break;
        case 2:
            Console.WriteLine("You selected the option 2. Add a new densist.");
            do
            {
                Console.Write("Enter the name of the new dentist: ");
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                    Console.WriteLine("The field cannot be empty.");


            } while (string.IsNullOrEmpty(name));

            Console.Write("Enter the phone number of the new dentist:");
            string receive = Console.ReadLine();
            while (!Int32.TryParse(receive, out telNum))
            {
                Console.Write("Not valid, try again: ");
                receive = Console.ReadLine();
            }

            //string telNum = Console.ReadLine();
            Dentist newDentist = new Dentist();
            newDentist.Name = name;
            newDentist.TelNum = telNum.ToString();
            using (SqlConnection conn = new SqlConnection(myConnection))
            {
                conn.Open();
                dentist.InsertDentist(conn, newDentist);
            }
            Console.WriteLine("Dentist added successfully!");
            break;
        case 3:
            Console.Write("Enter the name of the new dentist: ");
            string dentistNameToFind = Console.ReadLine();
            List<Dentist> dentistsToFind;
            using (SqlConnection conn = new SqlConnection(myConnection))
            {
                conn.Open();
                dentistsToFind = dentist.FindDentist(conn, dentistNameToFind);
            }
            if (dentistsToFind.Count > 0)
            {
                Console.WriteLine("Found " + dentistsToFind.Count + " dentist(s) with the name " + dentistNameToFind);
                foreach (Dentist d in dentistsToFind)
                {
                    Console.WriteLine("Id: " + d.Id + ", Name: " + d.Name + ", TelNum: " + d.TelNum);
                }
            }
            else
            {
                Console.WriteLine("Dentist with name " + dentistNameToFind + " not found!");
            }
            break;

        case 4:
            Console.Write("Enter the name of the dentist whose phone number you want to update:");
            string dentistName = Console.ReadLine();
            List<Dentist> dentistsToUpdate;
            using (SqlConnection conn = new SqlConnection(myConnection))
            {
                conn.Open();
                dentistsToUpdate = dentist.FindDentist(conn, dentistName);
            }
            if (dentistsToUpdate.Count > 0)
            {
                Console.WriteLine("Found " + dentistsToUpdate.Count + " dentist(s) with the name " + dentistName);
                foreach (Dentist d in dentistsToUpdate)
                {
                    Console.WriteLine("Id: " + d.Id + ", Name: " + d.Name + ", TelNum: " + d.TelNum);
                }
                Console.Write("Enter the Id of the dentist whose phone number you want to update:");
                int dentistId = int.Parse(Console.ReadLine());
                Console.Write("Enter the new phone number:");
                string newTelNum = Console.ReadLine();
                Dentist dentistToUpdate = dentistsToUpdate.Find(d => d.Id == dentistId);
                if (dentistToUpdate != null)
                {
                    dentistToUpdate.TelNum = newTelNum;
                    using (SqlConnection conn = new SqlConnection(myConnection))
                    {
                        conn.Open();
                        dentist.UpdateDentist(conn, dentistToUpdate);
                    }
                    Console.WriteLine("Dentist updated successfully!");
                }
                else
                {
                    Console.WriteLine("Dentist with Id " + dentistId + " not found!");
                }
            }
            else
            {
                Console.WriteLine("Dentist with name " + dentistName + " not found!");
            }
            break;


        case 5:
            Console.Write("Enter the name of the dentist you want to delete:");
            string dentistNameToDelete = Console.ReadLine();
            List<Dentist> dentistsToDelete;
            using (SqlConnection conn = new SqlConnection(myConnection))
            {
                conn.Open();
                dentistsToDelete = dentist.FindDentist(conn, dentistNameToDelete);
            }
            if (dentistsToDelete.Count > 0)
            {
                Console.WriteLine("Found " + dentistsToDelete.Count + " dentist(s) with the name " + dentistNameToDelete);
                foreach (Dentist d in dentistsToDelete)
                {
                    Console.WriteLine("Id: " + d.Id + ", Name: " + d.Name + ", TelNum: " + d.TelNum);
                }
                Console.Write("Enter the Id of the dentist you want to delete:");
                int dentistIdToDelete = int.Parse(Console.ReadLine());
                Dentist dentistToDelete = dentistsToDelete.Find(d => d.Id == dentistIdToDelete);
                if (dentistToDelete != null)
                {
                    using (SqlConnection conn = new SqlConnection(myConnection))
                    {
                        conn.Open();
                        dentist.DeleteDentist(conn, dentistToDelete);
                    }
                    Console.WriteLine("Dentist deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Dentist with Id " + dentistIdToDelete + " not found!");


                }
            }
            break;
        
    }
            Console.Write("More operations Y/N: ");
            received = Console.ReadLine().ToUpper();
            if (received.StartsWith("Y"))
                more = true;
            else
                more = false;

} while (more) ;
