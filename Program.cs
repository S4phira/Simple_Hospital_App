using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Hospital_Console_ClassLibrary;

namespace Hospital_Console
{
    class Program
    {
        static void Main(string[] args)
        {

            User aktywnyUser = new User("", "", 0, "", "");

            int przywilej = 0;
            string wybórUsera;
            bool run = true;
            Hospital Szpital = new Hospital();

            bool logowanie = true;
            Console.WriteLine("Wczytywanie");

            try
            {
                Szpital = Serializer.Load<Hospital>("C:\\Hospital_Managament_System-master\\Hospital_Console\\data2.bin");

            }
            catch (Exception)
            {
                Console.WriteLine("Brak danych");

            }
            Console.WriteLine("Witaj w naszym szpitalu");
            Console.WriteLine("Login: Pat  Hasło: Woj");




            while (logowanie)
            {
                Console.Write("Login: ");
                string _login = Console.ReadLine();
                Console.Write("Hasło: ");
                string _hasło = GetHiddenConsoleInput();

                Console.WriteLine("Wybierz typ użytkownika: [P]ielięgniarka, [A]dmin, [D]oktor");
                string wybór = Console.ReadLine();
                switch (wybór)
                {
                    case "P":
                        for (int i = 0; i < Szpital.Nurses.Count; i++)
                        {
                            if (Szpital.Nurses[i].password == _hasło && Szpital.Nurses[i].login == _login)
                            {
                                aktywnyUser = Szpital.Nurses[i];
                                przywilej = 0;
                                logowanie = false;
                                Console.Clear();
                                Console.WriteLine("Logowanie zakończone pomyślnie");
                                break;



                            }
                            else
                            {
                                Console.WriteLine("Nie ma takiego użytkownika");
                            }
                        }
                        break;

                    case "D":
                        for (int i = 0; i < Szpital.Doctors.Count; i++)
                        {
                            if (Szpital.Doctors[i].password == _hasło && Szpital.Doctors[i].login == _login)
                            {
                                aktywnyUser = Szpital.Doctors[i];
                                przywilej = 0;
                                logowanie = false;
                                Console.Clear();
                                Console.WriteLine("Logowanie zakończone pomyślnie");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Nie ma takiego użytkownika");
                            }
                        }
                        break;
                    case "A":
                        for (int i = 0; i < Szpital.Admins.Count; i++)
                        {
                            if (Szpital.Admins[i].password == _hasło && Szpital.Admins[i].login == _login)
                            {
                                aktywnyUser = Szpital.Admins[i];
                                przywilej = 1;
                                logowanie = false;
                                Console.Clear();
                                Console.WriteLine("Logowanie zakończone pomyślnie");
                                break;

                            }
                            else
                            {
                                Console.WriteLine("Nie ma takiego użytkownika");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Spróbuj ponownie!");

                        break;
                }
            }

            while (run)
            {
                Console.WriteLine("Witaj:" + " " + aktywnyUser.name);
                Console.WriteLine("Co chcesz wykonać?");
                Console.WriteLine("[S]prawdzić użytkowników?");
                Console.WriteLine("[Sp]rawdzić dyżur na ten miesiąc?");
                Console.WriteLine("[W]yjść i zapisać program?");
                if (przywilej > 0)
                {
                    Console.WriteLine("[D]odać nowego użytkownika?");
                    Console.WriteLine("[G]enerować dyżur dla pielęgiarek?");
                    Console.WriteLine("[Ge]nerować dyżur dla doktorów?");
                    Console.WriteLine("[Z]mienić dyżur dla pielęgniarek?");
                    Console.WriteLine("[Zm]ienić dyżur dla doktorów?");
                }
                wybórUsera = Console.ReadLine();
                switch (wybórUsera)
                {
                    case "Sp":
                        Console.WriteLine("Jakie nazwisko chcesz sprawdzić?");
                        string SprNazw = Console.ReadLine();
                        for (int i = 0; i < Szpital.NursesTimetable.Count; i++)
                        {
                            if (Szpital.NursesTimetable[i].sname == SprNazw)
                            {
                                Console.WriteLine("Dzień: " + i);
                            }
                        }
                        break;
                    case "Zm":
                        for (int i = 1; i < Szpital.monthd2.Length; i++)
                        {
                            Console.WriteLine("Dzień: " + i);
                            foreach (Doctor elem in Szpital.monthd2[i])
                            {
                                Console.WriteLine(elem.name + " " + elem.sname + " - " + elem.Spec);
                            }
                        }
                        Console.WriteLine("Podaj dzień do zmiany:");
                        int ZmianaD = Convert.ToInt32(Console.ReadLine());

                        int count2 = 0;
                        for (int i = 0; i < Szpital.monthd2[ZmianaD].Count; i++)
                        {
                            Console.WriteLine("Pracują: " + count2 + " " + Szpital.monthd2[ZmianaD][i].name + " " + Szpital.monthd2[ZmianaD][i].sname);
                            count2++;
                        }
                        Console.WriteLine("Proszę wybierz numer");
                        int WybNum = Convert.ToInt32(Console.ReadLine());
                        count2 = 0;
                        List<Doctor> dc1 = new List<Doctor>();
                        List<Doctor> dc2 = new List<Doctor>();
                        List<Doctor> dc3 = new List<Doctor>();
                        List<Doctor> dc4 = new List<Doctor>();
                        for (int o = 0; o < Szpital.monthd2[ZmianaD].Count; o++)
                        {
                            {
                                dc1.Add(Szpital.monthd2[ZmianaD][o]);
                            }
                        }
                        for (int o = 0; o < Szpital.monthd2[ZmianaD + 1].Count; o++)
                        {
                            {
                                dc2.Add(Szpital.monthd2[ZmianaD + 1][o]);
                            }
                        }
                        for (int o = 0; o < Szpital.monthd2[ZmianaD - 1].Count; o++)
                        {
                            {
                                dc3.Add(Szpital.monthd2[ZmianaD - 1][o]);
                            }
                        }

                        foreach (Doctor elem in Szpital.Doctors)
                        {
                            if (!dc1.Contains(elem) && !dc2.Contains(elem) && !dc3.Contains(elem) && elem.daysWorked <= 10)
                            {
                                Console.WriteLine("Sugestie do zastąpienia: " + count2 + " " + elem.name + " " + elem.sname);
                                dc4.Add(elem);
                                count2++;

                            }
                        }
                        Console.WriteLine("Proszę wybierz numer");
                        int WybNum2 = Convert.ToInt32(Console.ReadLine());
                        Szpital.monthd2[ZmianaD][WybNum] = dc4[WybNum2];

                        for (int i = 1; i < Szpital.monthd2.Length; i++)
                        {

                            Console.WriteLine("Dzień: " + i);
                            foreach (Doctor elem in Szpital.monthd2[i])
                            {
                                Console.WriteLine(elem.name + " " + elem.sname + " - " + elem.Spec);
                            }
                        }
                        break;
                    case "Z":
                        for (int i = 0; i < Szpital.NursesTimetable.Count; i++)
                        {
                            if (i > 0)
                            {
                                Console.WriteLine("Dzień: " + i + " " + Szpital.NursesTimetable[i].name + " " + Szpital.NursesTimetable[i].sname);
                            }
                        }
                        Console.WriteLine("Number dnia do zmiany:");
                        int changeDay = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Pracują: " + Szpital.NursesTimetable[changeDay].name + " " + Szpital.NursesTimetable[changeDay].sname);


                        int count = 1;
                        List<Nurse> s = new List<Nurse>();
                        for (int i = 0; i < Szpital.Nurses.Count; i++)
                        {
                            if (changeDay == 1)
                            {
                                if ((Szpital.Nurses[i].sname != Szpital.NursesTimetable[changeDay].sname) && (Szpital.Nurses[i].sname != Szpital.NursesTimetable[changeDay + 1].sname && Szpital.Nurses[i].daysWorked <= 9))
                                {
                                    Console.WriteLine("Sugestie do zastąpienia: " + count + " : " + Szpital.Nurses[i].name + " " + Szpital.Nurses[i].sname);
                                    s.Add(Szpital.Nurses[i]);
                                    count++;
                                }
                            }
                            else if (changeDay == 30)
                            {
                                if ((Szpital.Nurses[i].sname != Szpital.NursesTimetable[changeDay].sname) && (Szpital.Nurses[i].sname != Szpital.NursesTimetable[changeDay - 1].sname && Szpital.Nurses[i].daysWorked <= 9))
                                {
                                    Console.WriteLine("Sugestie do zastąpienia: " + count + " : " + Szpital.Nurses[i].name + " " + Szpital.Nurses[i].sname);
                                    s.Add(Szpital.Nurses[i]);
                                    count++;
                                }
                            }
                            else
                            {
                                if ((Szpital.Nurses[i].sname != Szpital.NursesTimetable[changeDay - 1].sname) && (Szpital.Nurses[i].daysWorked <= 9) && (Szpital.Nurses[i].sname != Szpital.NursesTimetable[changeDay].sname) && (Szpital.Nurses[i].sname != Szpital.NursesTimetable[changeDay + 1].sname))
                                {

                                    Console.WriteLine("Sugestie do zastąpienia: " + count + " : " + Szpital.Nurses[i].name + " " + Szpital.Nurses[i].sname);
                                    s.Add(Szpital.Nurses[i]);
                                    count++;
                                }
                            }
                        }
                        Console.WriteLine("Proszę wybierz numer");
                        int changeName = Convert.ToInt32(Console.ReadLine());
                        Szpital.NursesTimetable[changeDay] = s[changeName - 1];
                        for (int i = 0; i < Szpital.NursesTimetable.Count; i++)
                        {
                            if (i > 0)
                            {
                                Console.WriteLine("Dzień: " + i + " " + Szpital.NursesTimetable[i].name + " " + Szpital.NursesTimetable[i].sname);
                            }
                        }
                        break;
                    case "W":
                        run = false;
                        break;
                    case "D":
                        string test = "";
                        string _n = "", _sn = "", _l = "", _p = "", _spec = "";
                        long _sid = 0;
                        bool inputcheck = true;
                        while (inputcheck)
                        {
                            try
                            {
                                Console.WriteLine("Podaj imię użytkownika:");
                                _n = Console.ReadLine();
                                Console.WriteLine("Podaj nazwisko użytkownika:");
                                _sn = Console.ReadLine();
                                if (_n.Length > 0 && _n.Length < 15 && _sn.Length > 0 && _sn.Length < 25)
                                {

                                    inputcheck = false;
                                    break;
                                }
                            }
                            catch { }
                            Console.WriteLine("Spróbuj ponownie");
                        }
                        inputcheck = true;

                        inputcheck = true;
                        while (inputcheck)
                        {
                            try
                            {
                                Console.WriteLine("Podaj numer PESEL:");
                                _sid = Convert.ToInt64(Console.ReadLine());
                                if (_sid.ToString().Length == 11)
                                {
                                    inputcheck = false;
                                    break;
                                }
                            }
                            catch
                            { }
                            Console.WriteLine("Zły numer, Spróbuj ponownie");
                        } while (inputcheck)
                        {
                            try
                            {
                                Console.WriteLine("Podaj login użytkownika:");
                                _l = Console.ReadLine();
                                Console.WriteLine("Podaj hasło użytkownika");
                                _p = Console.ReadLine();
                                if (_l.Length > 0 && _l.Length < 15 && _p.Length > 0 && _p.Length < 25)
                                {
                                    inputcheck = false;
                                    break;
                                }
                            }
                            catch { }
                            Console.WriteLine("Please Spróbuj ponownie");
                        }


                        Console.WriteLine("Jaki typ użytkownika chcesz stwozyć? [P]ielęgniarka, [D]oktor, [A]dmin");
                        string wybór = Console.ReadLine();
                        switch (wybór)
                        {
                            case "D":

                                bool inputcheck1 = true;
                                int _whz = 0;
                                while (inputcheck1)
                                {
                                    try
                                    {
                                        Console.WriteLine("Podaj numer WHZ");
                                        _whz = Convert.ToInt32(Console.ReadLine());
                                        if (_whz.ToString().Length == 3)
                                        {
                                            inputcheck1 = false;
                                            break;
                                        }
                                    }
                                    catch { }
                                    Console.WriteLine("Zły numer, Spróbuj ponownie");
                                }

                                inputcheck = true;
                                while (inputcheck)
                                {
                                    try
                                    {
                                        Console.WriteLine("Podaj specjalizacje: (neurolog, laryngolog, kardiolog, urolog)");
                                        _spec = Console.ReadLine();
                                        if (_spec == "neurolog" || _spec == "kardiolog" || _spec == "urolog" || _spec == "laryngolog")
                                        {
                                            inputcheck = false;
                                            break;
                                        }
                                    }
                                    catch { }
                                    Console.WriteLine("Spróbuj ponownie");
                                }
                                Doctor d = new Doctor(_n, _sn, _sid, _l, _p, _whz, _spec);
                                Szpital.Doctors.Add(d);
                                break;
                            case "P":
                                Nurse k = new Nurse(_n, _sn, _sid, _l, _p);
                                Szpital.Nurses.Add(k);
                                break;
                            case "A":
                                Admin z = new Admin(_n, _sn, _sid, _l, _p);
                                Szpital.Admins.Add(z);
                                break;

                        }

                        break;
                    case "S":
                        Console.WriteLine("Nasi doktorzy: ");
                        for (int i = 0; i < Szpital.Doctors.Count; i++)
                        {
                            Console.WriteLine(Szpital.Doctors[i].name + " " + Szpital.Doctors[i].sname + " - " + Szpital.Doctors[i].Spec);
                        }

                        Console.WriteLine("Nasze Pielęgniarki: ");

                        for (int i = 0; i < Szpital.Nurses.Count; i++)
                        {
                            Console.WriteLine(Szpital.Nurses[i].name + " " + Szpital.Nurses[i].sname);

                        }

                        if (przywilej > 0)
                        {
                            Console.WriteLine("Nasi Admini:");
                            for (int i = 0; i < Szpital.Admins.Count; i++)
                            {
                                Console.WriteLine(Szpital.Admins[i].name + " " + Szpital.Admins[i].sname);

                            }
                            Console.WriteLine("Wciśnij dowolny przycisk...");
                            Console.ReadKey();
                            break;
                        }
                        break;

                    case "G":

                        Random rnd = new Random();
                        bool generating = true;

                        int l = rnd.Next(Szpital.Nurses.Count);
                        Szpital.NursesTimetable.Add(Szpital.Nurses[l]);

                        for (int i = 1; i < 31; i++)
                        {

                            generating = true;
                            while (generating)
                            {

                                int r = rnd.Next(Szpital.Nurses.Count);


                                if (Szpital.Nurses[r].daysWorked <= 9 && Szpital.Nurses[r].socialID != Szpital.NursesTimetable[i - 1].socialID)
                                {
                                    Szpital.NursesTimetable.Add(Szpital.Nurses[r]);
                                    generating = false;
                                    Szpital.Nurses[r].daysWorked++;

                                }


                            }

                        }

                        for (int i = 0; i < Szpital.NursesTimetable.Count; i++)
                        {
                            if (i > 0)
                            {
                                Console.WriteLine("Dzień: " + i + " " + Szpital.NursesTimetable[i].name + " " + Szpital.NursesTimetable[i].sname);
                            }
                        }
                        for (int i = 0; i < Szpital.Nurses.Count; i++)
                        {
                            Console.WriteLine(Szpital.Nurses[i].name + " " + "pracowała: " + " " + Szpital.Nurses[i].daysWorked );
                        }


                        break;

                    case "Ge":
                        Console.WriteLine("Dla ilu doktorów dziennie chcesz wygenerować dyżur?");

                        int LiczbaDok = Convert.ToInt32(Console.ReadLine());

                        if (Szpital.Doctors.Count < LiczbaDok * 3)
                        {
                            Console.WriteLine("Potrzeba więcej doktorów, prosze dodaj użytkownika");
                            break;

                        }
                        //List<Doctor>[] Szpital.monthd2 = new List<Doctor>[30];
                        for (int i = 0; i < Szpital.monthd2.Length; i++)
                        {
                            Szpital.monthd2[i] = new List<Doctor>();
                        }
                        int generations = 0;
                        bool generating2;
                        Random rndd = new Random();
                        Console.WriteLine("Generowanie dyżuru na 30 dni: ");
                        for (int i = 1; i < Szpital.monthd2.Length; i++)
                        {
                            generations = 0;
                            // Console.WriteLine("Generating...." + i);
                            generating2 = true;
                            string speccheck = "";
                            while (generating2)
                            {

                                int r = rndd.Next(Szpital.Doctors.Count);


                                Doctor TempDoctor = Szpital.Doctors[r];
                                // Console.WriteLine(TempDoctor.name);

                                if (Szpital.Doctors[r].daysWorked < 9)
                                {
                                    //Console.WriteLine("doctor" + TempDoctor.name + " " + "has passed the days worked check");
                                    if (!(Szpital.monthd2[i - 1].Contains(Szpital.Doctors[r])))
                                    {
                                        //Console.WriteLine("doctor" + TempDoctor.name + " " + "has passed the - index check");
                                        //Console.WriteLine(speccheck);
                                        if (!(speccheck.Contains(Szpital.Doctors[r].Spec)))
                                        {
                                            // Console.WriteLine("doctor" + TempDoctor.name + " " + "has passed all the checks");
                                            Szpital.Doctors[r].daysWorked++;
                                            generations++;
                                            Szpital.monthd2[i].Add(Szpital.Doctors[r]);

                                            speccheck += Szpital.Doctors[r].Spec.ToString();
                                            //Console.WriteLine(speccheck);


                                            if (generations == LiczbaDok)
                                            {
                                                generating = false;
                                                break;
                                            }
                                        }

                                    }

                                }

                            }

                        }
                        for (int i = 0; i < Szpital.monthd2.Length; i++)
                        {
                            //  Console.WriteLine(i + " " + "dzień");
                            for (int k = 0; k < Szpital.monthd2[i].Count; k++)
                            {
                                Console.WriteLine("Dzień:" + i + " " + Szpital.monthd2[i][k].name + " " + Szpital.monthd2[i][k].Spec + " ");
                            }
                        }
                        for (int i = 0; i < Szpital.Doctors.Count; i++)
                        {
                            Console.WriteLine(Szpital.Doctors[i].name + " " + "Pracował[a]" + " " + Szpital.Doctors[i].daysWorked );
                        }
                        break;
                    default:
                        Console.WriteLine("Spróbuj ponownie");
                        break;
                }
            }



            Console.WriteLine("Zapisywanie....");
            Serializer.Save("C:\\Hospital_Managament_System-master\\Hospital_Console\\data2.bin", Szpital);
            Console.WriteLine("Zapisano!!");


        }
        private static string GetHiddenConsoleInput()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            return input.ToString();
        }
        static void printList(List<Doctor> li)
        {
            for (int i = 0; i < li.Count; i++)
            {
                Console.WriteLine(li[i].name + " " + li[i].sname);
            }
        }
        public class Serializer
        {
            public static void Save(string filePath, object objToSerialize)
            {
                try
                {
                    using (Stream stream = File.Open(filePath, FileMode.Create))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, objToSerialize);
                    }
                }
                catch (IOException)
                {
                }
            }

            public static T Load<T>(string filePath) where T : new()
            {
                T rez = new T();

                try
                {
                    using (Stream stream = File.Open(filePath, FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        rez = (T)bin.Deserialize(stream);
                    }
                }
                catch (IOException)
                {
                }

                return rez;
            }
        }


    }

}
