﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public sealed class GameMaster
    {
        private static readonly GameMaster instance = new GameMaster();
        private string SongsDirectory = @"Resources\Songs\";

        public static GameMaster Instance
        {
            get { return instance; }
        }
        public List<Song> Songs { get; private set; }
        public List<Player> Players { get; private set; }
        public Game Game { get; private set; }
        public Boolean UpToDateGame { get; private set; }
        public SheetBuilder SheetBuilder { get; private set; }

        private GameMaster()
        {
            this.Songs = this.LoadSongs();
            this.Players = new List<Player>();
            this.UpToDateGame = false;
            this.SheetBuilder = new SheetBuilder(this.SongsDirectory);
        }

        private List<Song> LoadSongs()
        {
            List<Song> songs = new List<Song>();
            string[] dirs = Directory.GetDirectories(this.SongsDirectory);
            int index = 0;
            foreach (string songDirectory in dirs)
            {
                string title = null, author = null, year = null;
                Uri file = null;
                string metadata = Path.Combine(songDirectory, "meta.data");
                string musicFile = Path.Combine(songDirectory, "music.mp3");

                // Check for metadata
                if (File.Exists(metadata)) {
                    string[] lines = File.ReadAllLines(metadata);
                    if (lines.Length == 3)
                    {
                        title = lines[0];
                        author = lines[1];
                        year = lines[2];
                    }
                }

                if (File.Exists(musicFile))
                {
                    file = new Uri(musicFile, UriKind.Relative);
                }

                // If metadata and music file were found, we can add sheets
                if (title != null && file != null)
                {
                    List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
                    string[] sheetsFiles = Directory.GetFiles(songDirectory, "*.sheet");
                    foreach (string sheet in sheetsFiles)
                    {
                        string[] split = Path.GetFileNameWithoutExtension(sheet).Split('-');
                        string instr = split[0];
                        string diff = split[1];
                        Instrument instrument = (Instrument)Enum.Parse(typeof(Instrument), instr, true);
                        Difficulty difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), diff, true);
                        sheets.Add(Tuple.Create(instrument, difficulty));
                    }

                    songs.Add(new Song(title, author, year, sheets, file, index));
                    index++;
                }
            }

            return songs;
        }

        public void SelectSong(Song song)
        {
            this.Game = new Game(song);
            this.UpToDateGame = true;

            foreach (Player player in this.Players)
            {
                player.InformNewGame();
            }
        }

        // Attribute ok ? See later
        public void NewPlayer(Player player)
        {
            this.Players.Add(player);
            if (this.UpToDateGame)
                player.InformNewGame();
        }

        public void NewPlayer()
        {
            Player player = new Player();
            this.NewPlayer(player);
        }

        // Called by a player > at least one player existing and ready
        // A revoir (appelé par la VM ?)
        public void Ready(bool autolaunch = true)
        {
            Boolean everyoneReady = true;
            foreach (Player player in this.Players)
            {
                everyoneReady &= player.Ready;
            }

            if (!autolaunch)
            {
                return;
            }

            if (everyoneReady && !this.Game.IsPlaying)
            {
                this.Game.Launch();
            }
            else if (everyoneReady && this.Game.IsPlaying)
            {
                // Place the sheet reader to the right part of the sheet then play 
            }
        }
    }
}