using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CaseStudy_Q
{
    class Program
    {
        static void Main(string[] args)
        {
            string s;
            TPManager t = new TPManager();

            do
            {
                Console.WriteLine("Hello welcome to Tariff Plan Management Portal");
                Console.WriteLine("1 \t to Add");
                Console.WriteLine("2 \t to Update");
                Console.WriteLine("3 \t to Delete");
                Console.WriteLine("4 \t to View");
                int switch_on = Convert.ToInt32(Console.ReadLine());
                switch (switch_on)
                {
                    case 1:
                        t.AddPlan();
                        break;
                    case 2:
                        t.UpdatePlan();
                        break;
                    case 3:
                        t.DeletePlan();
                        break;
                    case 4:
                        t.ViewPlan();
                        break;

                    default:
                        Console.WriteLine("Wrong Key pressed");
                        break;
                }
                Console.WriteLine("Do you want to exit? (y/n)");
                s = (Console.ReadLine().ToLower());

            } while (s.ToLower().Equals("n"));
        }
    }

    //Tariff Plan Management Portal

    class TariffPlan
    {
        public int TPlanId { get; set; }
        public string PlanName { get; set; }
        public string TypeOf { get; set; }
        public string TariffRate { get; set; }
        public int Validity { get; set; }
        public string Rental { get; set; }

        public TariffPlan(string planName, string typeOf, string tariffRate, int validity, string rental)
        {
            TPlanId = GenId.idGen();
            PlanName = planName;
            TypeOf = typeOf;
            TariffRate = tariffRate;
            Validity = validity;
            Rental = rental;
        }
    }

    class TPManager
    {
        //List<TariffPlan> plans = new List<TariffPlan>();
        public int AddPlan()
        {
            int id = 0;
            Console.WriteLine("Enter Plan Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Plan Type");
            string type = Console.ReadLine();
            Console.WriteLine("Enter Plan Rate");
            string rate = Console.ReadLine();
            Console.WriteLine("Enter Plan Validity");
            int vali = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Plan Rental (if any)");
            string rent = Console.ReadLine();
            /*if (type.ToLower() = "voice")
            {
                Console.WriteLine("Type is not appropriate. Re-enter the details.");
                return 0;
            }
            else if (type.ToLower() != "data")
            {
                Console.WriteLine("Type is not appropriate. Re-enter the details.");
                return 0;
            }*/
            TariffPlan t = new TariffPlan(name, type, rate, vali, rent);
            SqlConnection con = null;
            try
            {
                // Creating Connection
                //Data Source=(LocalDB)\MSSQLLocalDB;database=C:\Users\Chetan\Documents\forCaseStudy.mdf;Integrated Security=True;Connect Timeout=30
                con = new SqlConnection("data source=(LocalDB)\\MSSQLLocalDB; database=C:\\USERS\\CHETAN\\DOCUMENTS\\FORCASESTUDY.MDF; integrated security=true");
                // writing sql query  
                string query = "insert into [Tariff Plan ] (id,PlanName ,TypeOfPlan, TariffRate , validity , Rental )values(" + t.TPlanId +
                    ", '" + t.PlanName + "','" + t.TypeOf + "' , '" + rate + "'," + t.Validity + "," + t.Rental + ")";
                SqlCommand cm = new SqlCommand(query, con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();
                // Displaying a message  
                Console.WriteLine("Plan is added");
                id = t.TPlanId;
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }


            return 0;
        }

        public void DeletePlan()
        {

            Console.WriteLine("Enter Plan id to delete");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=(LocalDB)\\MSSQLLocalDB; database=C:\\USERS\\CHETAN\\DOCUMENTS\\FORCASESTUDY.MDF; integrated security=true");
                string vquery = "select * from [Tariff Plan] where id= " + id;
                string dquery = "delete from [Tariff Plan ] where id = " + id + "";
                SqlCommand cm = new SqlCommand(vquery, con);
                con.Open();
                SqlDataReader sdr = cm.ExecuteReader();
                while (sdr.Read())
                {
                    Console.WriteLine(sdr["id"] + "\t|" + sdr["PlanName"] + "\t|" + sdr["TypeOfPlan"] + "\t|" + sdr["TariffRate"] + "\t|" + sdr["Validity"] + "\t|" + sdr["Rental"]); // Displaying Record  
                }
                Console.WriteLine("Do you want to exit? (y/n)");
                string s = (Console.ReadLine().ToLower());
                if (s == "y")
                {
                    SqlConnection con2 = new SqlConnection("data source=(LocalDB)\\MSSQLLocalDB; database=C:\\USERS\\CHETAN\\DOCUMENTS\\FORCASESTUDY.MDF; integrated security=true");
                    SqlCommand deleteCom = new SqlCommand(dquery, con2);
                    con2.Open();
                    int row = deleteCom.ExecuteNonQuery();
                    Console.WriteLine(row);
                    Console.WriteLine("Plan is Deleted");
                    con2.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }

        }

        public void UpdatePlan()
        {
            Console.WriteLine("You want to update plan. by which you want to search?");
            Console.WriteLine("1 \t By Id");
            Console.WriteLine("2 \t By Name");
            int switch_on = Convert.ToInt32(Console.ReadLine());
            switch (switch_on)
            {
                case 1:
                    ByIdUpdate();
                    break;
                case 2:
                    ByNameUpdate();
                    break;
               
                default:
                    Console.WriteLine("Wrong Key pressed");
                    break;
            }


        }

        void ByIdUpdate()
        {
            Console.WriteLine("Enter Plan id to Update");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You want to update plan. What want to update?");
            Console.WriteLine("1 \t By Plan Name");
            Console.WriteLine("2 \t By Plan Type");
            Console.WriteLine("3 \t By Plan Tariff Rate");
            Console.WriteLine("4 \t By Plan Validity");
            Console.WriteLine("5 \t By Plan Rental");
            int switch_on = Convert.ToInt32(Console.ReadLine());

            string query = null;
            switch (switch_on)
            {
                case 1:
                    Console.WriteLine("enter name to update");
                    string nameToChange = (Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + nameToChange + "' where id=" + id + "";

                    break;
                case 2:
                    Console.WriteLine("enter name to update");
                    string typeToChange = (Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + typeToChange + "' where id=" + id + "";

                    break;
                case 3:
                    Console.WriteLine("enter name to update");
                    string rateToChange = (Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + rateToChange + "' where id=" + id + "";

                    break;
                case 4:
                    Console.WriteLine("enter name to update");
                    int validityToChange = Convert.ToInt32(Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + validityToChange + "' where id=" + id + "";

                    break;
                case 5:
                    Console.WriteLine("enter name to update");
                    string rentalToChange = (Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + rentalToChange + "' where id=" + id + "";

                    break;


                default:
                    Console.WriteLine("Wrong Key pressed");
                    break;
            }
            if (query != null)
            {
                SqlConnection con = null;

                try
                {
                    con = new SqlConnection("data source=(LocalDB)\\MSSQLLocalDB; database=C:\\USERS\\CHETAN\\DOCUMENTS\\FORCASESTUDY.MDF; integrated security=true");
                    SqlCommand cm = new SqlCommand(query, con);
                    con.Open();
                    int i = cm.ExecuteNonQuery();
                    if (i == 1)
                    {
                        Console.WriteLine("Record Updated Successfully"+i);
                    }
                    else
                    {
                        Console.WriteLine("Record not found");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("OOPs, something went wrong." + e);
                }
                // Closing the connection  
                finally
                {
                    con.Close();
                }
            }


        }

        void ByNameUpdate()
        {
            Console.WriteLine("Enter Plan Name to Update");
            string name = (Console.ReadLine());
            Console.WriteLine("You want to update plan. What want to update?");
            Console.WriteLine("1 \t By Plan Name");
            Console.WriteLine("2 \t By Plan Type");
            Console.WriteLine("3 \t By Plan Tariff Rate");
            Console.WriteLine("4 \t By Plan Validity");
            Console.WriteLine("5 \t By Plan Rental");
            int switch_on = Convert.ToInt32(Console.ReadLine());
            string query = null;
            switch (switch_on)
            {
                case 1:
                    Console.WriteLine("enter name to update");
                    string nameToChange = (Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + nameToChange + "' where PlanName like '" + name + "' ";
                    //update[Tariff Plan] set PlanName = 'Diwali' where PlanName like 'Holi';
                    break;
                case 2:
                    Console.WriteLine("enter name to update");
                    string typeToChange = (Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + typeToChange + "' where PlanName like '" + name + "' ";

                    break;
                case 3:
                    Console.WriteLine("enter name to update");
                    string rateToChange = (Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + rateToChange + "' where PlanName like '" + name + "' ";

                    break;
                case 4:
                    Console.WriteLine("enter name to update");
                    int validityToChange = Convert.ToInt32(Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + validityToChange + "' where PlanName like '" + name + "' ";

                    break;
                case 5:
                    Console.WriteLine("enter name to update");
                    string rentalToChange = (Console.ReadLine());
                    query = "update [Tariff Plan ] set PlanName = '" + rentalToChange + "' where PlanName like '" + name + "' ";

                    break;


                default:
                    Console.WriteLine("Wrong Key pressed");
                    break;
            }

            if (query != null)
            {
                SqlConnection con = null;

                try
                {
                    con = new SqlConnection("data source=(LocalDB)\\MSSQLLocalDB; database=C:\\USERS\\CHETAN\\DOCUMENTS\\FORCASESTUDY.MDF; integrated security=true");
                    SqlCommand cm = new SqlCommand(query, con);
                    con.Open();
                    int i = cm.ExecuteNonQuery();
                    if (i == 1)
                    {
                        Console.WriteLine("Record Updated Successfully" + i);
                    }
                    else
                    {
                        Console.WriteLine("Record not found");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("OOPs, something went wrong." + e);
                }
                // Closing the connection  
                finally
                {
                    con.Close();
                }
            }

        }

        public void ViewPlan()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=(LocalDB)\\MSSQLLocalDB; database=C:\\USERS\\CHETAN\\DOCUMENTS\\FORCASESTUDY.MDF; integrated security=true");
                SqlCommand cm = new SqlCommand("select * from [Tariff Plan]", con);
                con.Open();
                SqlDataReader sdr = cm.ExecuteReader();
                Console.WriteLine("ID \t|" + "NAME \t|" + "TYPEOF \t|" + "TARIFFRATE \t|" + "VALIDITY \t| RENTAL"); // Displaying Record  

                while (sdr.Read())
                {
                    Console.WriteLine(sdr["id"] + "\t|" + sdr["PlanName"] + "\t|" + sdr["TypeOfPlan"] + "\t|" + sdr["TariffRate"] + "\t|" + sdr["Validity"] + "\t|" + sdr["Rental"]); // Displaying Record  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }

        }
    }

    class GenId
    {
        public static int idGen()
        {
            DateTime dt = DateTime.Now;
            string id = Convert.ToString(dt.Day + "" + dt.Month + "" + dt.Hour + "" + dt.Minute);
            int inId = Convert.ToInt32(id);
            /*Console.WriteLine(inId + "  " + id);*/
            return inId;
        }
    }
}
