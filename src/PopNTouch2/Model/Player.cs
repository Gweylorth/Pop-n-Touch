﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PopNTouch2.Model
{
    public class Player
    {
        /*private GameMaster gameMaster;
        private SheetMusic sheetmusic;
        private Game currentGame;
        private Boolean ready;
        private List<Instrument> availableInstruments;
        private Instrument instrument;
        private Difficulty difficulty; */

        public SheetMusic SheetMusic { get; set; }
        public Game CurrentGame { get; set; }
        public Boolean Ready { get; set; }
        public Instrument Instrument { get; set; }
        public Difficulty Difficulty { get; set; }
        public Timer Timer { get; set; }
        List<Tuple<double, Note>>.Enumerator Enumerator { get; set; }

        public class NoteTicked : EventArgs
        {
            public Note Note { get; set; }
        }
        public event TickHandler Tick;
        public delegate void TickHandler(Player p, NoteTicked nt);

        public Player() { }

        public void InformNewGame()
        {
            this.CurrentGame = GameMaster.Instance.Game;
            this.Ready = false;
        }

        public void IMReady()
        {
            this.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, Instrument, Difficulty);
            this.Ready = true;
            GameMaster.Instance.Ready();
        }

        public void NotReadyAnymore()
        {
            this.Ready = false;
        }

        public void ReadSheet()
        {
            this.Enumerator = this.SheetMusic.Notes.GetEnumerator();
            this.Timer = new Timer(this.SheetMusic.TimeRest);
            this.Timer.AutoReset = false;
            this.Timer.Elapsed += new ElapsedEventHandler(OnTimerTicked);
            this.Timer.Start();
        }

        public void OnTimerTicked(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Tick");
            if (Tick != null)
            {
                NoteTicked nt = new NoteTicked();
                nt.Note = this.Enumerator.Current.Item2;
                Console.WriteLine("Tack");
                Tick(this, nt);
            }
            if (this.Enumerator.MoveNext())
            {
                // ??
                this.Timer.Interval = this.Enumerator.Current.Item1;
                this.Timer.Start();
            }
        }
    }
}
