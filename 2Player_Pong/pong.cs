using System;
using System.Threading;


namespace _2Player_Pong
{
    class pong
    {

        // Başlangıç tanımlamaları
        int genislik, yukseklik;
        Alan alan;
        sopa sopa1;
        sopa sopa2;
        top top;

        ConsoleKeyInfo keyInfo;
        ConsoleKey consoleKey;

        public pong(int genislik, int yukseklik)
        {
            this.genislik = genislik;
            this.yukseklik = yukseklik;
            alan = new Alan(genislik, yukseklik);
            top = new top(genislik / 2, yukseklik / 2, yukseklik, genislik);
        }

        public void Kur()
        {
            sopa1 = new sopa(2, yukseklik);
            sopa2 = new sopa(genislik - 2, yukseklik);
            keyInfo = new ConsoleKeyInfo();
            consoleKey = new ConsoleKey();

            top.X = genislik / 2;
            top.Y = yukseklik / 2;
            top.yon = 0;

        }

        //Oyun sonu skor yazdırma
        public void skor()
        {
            top.skor_Hesapla();
            if (top.skor1 == 3 || top.skor2 == 3)
            {
                Console.Clear();
                Console.WriteLine("First Player: " + top.skor1.ToString() + "\t : \t" + "Second player: " + top.skor2.ToString());
                devam();

            }
        }

        void Giris()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                consoleKey = keyInfo.Key;
            }
        }


        //Oyun alanı oluşturma ve tuş takımı atama
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                skor();
                Kur();
                alan.ciz();
                sopa1.ciz();
                sopa2.ciz();
                top.ciz();


                while (top.X != 1 && top.X != genislik - 1)
                {
                    Giris();
                    switch (consoleKey)
                    {
                        case ConsoleKey.W:
                            sopa1.yukarı();
                            break;

                        case ConsoleKey.S:
                            sopa1.asagi();
                            break;

                        case ConsoleKey.UpArrow:
                            sopa2.yukarı();
                            break;

                        case ConsoleKey.DownArrow:
                            sopa2.asagi();
                            break;
                    }

                    consoleKey = ConsoleKey.A;
                    top.mantik(sopa1, sopa2);
                    top.ciz();
                    Thread.Sleep(20);
                }
            }
        }

        //Oyun sonu devam durumu 
        public void devam()
        {
            while (true)
            {
                Console.Write("Do you want to continue? (Y/N) ");
                string cevap = Console.ReadLine();

                if (cevap == "Y" || cevap == "y")
                {
                    Run();
                    Console.Write("Do you want to continue? (Y/N) ");
                    cevap = Console.ReadLine();

                }
                else if (cevap == "N" || cevap == "n")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.Write("Do you want to continue? (Y/N) ");
                    cevap = Console.ReadLine();
                }

            }
        }
    }

    //Oyuncu nesnelerini tanımlama ve oluşturma
    class sopa
    {
        public int X { get; set; }
        public int Y { get; set; }


        public int uzunluk { get; set; }
        public int alanYuseklik { get; set; }

        public sopa(int x, int alanYukseklik)
        {
            this.alanYuseklik = alanYukseklik;
            X = x;
            Y = alanYukseklik / 2;
            uzunluk = alanYukseklik / 3;

        }

        public void yukarı()
        {

            if ((Y - 1 - (uzunluk / 2)) != 0)
            {
                Console.SetCursorPosition(X, (Y + (uzunluk / 2)) - 1);
                Console.Write("\0");
                Y--;
                ciz();
            }


        }

        public void asagi()
        {
            if ((Y + 1 + (uzunluk / 2)) != alanYuseklik + 2)
            {
                Console.SetCursorPosition(X, (Y - (uzunluk / 2)));
                Console.Write("\0");
                Y++;
                ciz();
            }
        }

        public void ciz()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            for (int i = (Y - (uzunluk / 2)); i < (Y + (uzunluk / 2)); ++i)
            {
                Console.SetCursorPosition(X, i);
                Console.Write("│");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

    }

    //Oyun içi top tanımlama, oluşturma ve hareketini belirleme
    class top
    {
        public int X { set; get; }
        public int Y { set; get; }
        public int skor1 { get; set; }
        public int skor2 { get; set; }


        int donusX, donusY;
        int alanYukseklik, alanGenislik;
        public int yon { set; get; }

        public top(int x, int y, int alanyukseklik, int alangenislik)
        {
            X = x;
            Y = y;

            alanYukseklik = alanyukseklik;
            alanGenislik = alangenislik;
            donusX = -1;
            donusY = 1;
        }

        public void ciz()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(X, Y);
            Console.Write("o");
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public void mantik(sopa sopa1, sopa sopa2)
        {
            Console.SetCursorPosition(X, Y);
            Console.WriteLine("\0");

            if (Y <= 1 || Y >= alanYukseklik)
            {
                donusY *= -1;
            }
            if ((X == 3) && (sopa1.Y - (sopa1.uzunluk / 2)) <= Y && (sopa1.Y + (sopa1.uzunluk / 2)) > Y)
            {

                donusX *= -1;
                if (Y == sopa1.Y)
                {
                    yon = 0;

                }
                if ((Y >= (sopa1.Y - (sopa1.uzunluk / 2)) && Y < sopa1.Y) || (Y > sopa1.Y && Y < (sopa1.Y + (sopa1.uzunluk / 2))))
                {
                    yon = 1;
                }


            }

            if ((X == alanGenislik - 3) && (sopa2.Y - (sopa2.uzunluk / 2)) <= Y && (sopa2.Y + (sopa2.uzunluk / 2)) > Y)
            {
                donusX *= -1;

                if (Y == sopa2.Y)
                {
                    yon = 0;
                }
                if ((Y >= (sopa2.Y - (sopa2.uzunluk / 2)) && Y < sopa2.Y) || (Y > sopa2.Y && Y < (sopa2.Y + (sopa2.uzunluk / 2))))
                {
                    yon = 1;
                }

            }

            switch (yon)
            {
                case 0:
                    X += donusX;

                    break;

                case 1:
                    X += donusX;
                    Y += donusY;

                    break;
            }

        }
        public void skor_Hesapla()
        {
            if (X == alanGenislik - 1)
            {
                skor1++;
            }
            if (X == 1)
            {
                skor2++;
            }
        }


    }

    //Oyun alanı tanımlama ve çizdirme
    public class Alan
    {

        public int yukseklik { get; set; }
        public int genislik { get; set; }

        public Alan()
        {
            yukseklik = 25;
            genislik = 75;


        }
        public Alan(int genislik, int yukseklik)
        {
            this.genislik = genislik;
            this.yukseklik = yukseklik;
        }

        public void ciz()
        {

            for (int i = 0; i <= genislik + 1; ++i)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("─");

            }
            for (int i = 0; i <= genislik + 1; ++i)
            {
                Console.SetCursorPosition(i, (yukseklik + 1));
                Console.Write("─");
            }

            for (int i = 0; i <= yukseklik + 1; ++i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│");

            }
            for (int i = 0; i <= yukseklik + 1; ++i)
            {
                Console.SetCursorPosition((genislik + 1), i);
                Console.Write("│");
            }

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("┌");
            Console.SetCursorPosition(genislik + 1, 0);
            Console.WriteLine("┐");
            Console.SetCursorPosition(0, yukseklik + 1);
            Console.WriteLine("└");
            Console.SetCursorPosition(genislik + 1, yukseklik + 1);
            Console.WriteLine("┘");


        }

    }
}
