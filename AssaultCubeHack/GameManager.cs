﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AssaultCubeHack
{

    public class GameManager
    {

        public struct Offsets
        {
            public int viewMatrix, playerArrayOffset, numberOfPlayersOffset, localPlayer;

            public Offsets(int viewMatrix, int playerArrayOffset, int numberOfPlayersOffset, int localPlayer)
            {
                this.viewMatrix = viewMatrix;
                this.playerArrayOffset = playerArrayOffset;
                this.numberOfPlayersOffset = playerArrayOffset + numberOfPlayersOffset;
                this.localPlayer = localPlayer;
            }
        }

        public Offsets offsets = new Offsets(
                0x101AE8,
                0x10F4F8,
                0x08,
                0x10F4F4
            );

        public int baseAddress;
        public Memory pm;

        public Player[] players;
        public Player localPlayer;
        public byte[] viewMatrix;

        public Dictionary<LocatableEntity, LocatableEntity[]> espEntities;

        public GameManager(int baseAddress, Memory pm)
        {
            this.baseAddress = baseAddress;
            this.pm = pm;
        }

        public void startPlayerThread()
        {
            Thread thread = new Thread(new ThreadStart(loopPlayerLoad));
            thread.Start();
            thread.IsBackground = true;
        }

        public void loopPlayerLoad()
        {
            while (true)
            {
                loadViewMatrix();
                loadNonLocalPlayers();
                loadLocalPlayer();
            }
        }

        public void loadViewMatrix()
        {
            this.viewMatrix = pm.ReadMatrix(this.baseAddress + this.offsets.viewMatrix);
        }

        public void loadLocalPlayer()
        {
            if (localPlayer == null)
            {
                localPlayer = new Player(this.baseAddress + this.offsets.localPlayer, new int[] { 0x0 }, pm);
                espEntities = new Dictionary<LocatableEntity, LocatableEntity[]>();
                espEntities.Add(localPlayer, players);
            }

            localPlayer.loadPlayerData();
        }

        public void loadNonLocalPlayers()
        {
            int playerArray = pm.ReadInt(this.baseAddress + this.offsets.playerArrayOffset);
            int numberOfPlayers = pm.ReadInt(this.baseAddress + this.offsets.numberOfPlayersOffset);


            if (numberOfPlayers > 0)
            {
                if (players == null || numberOfPlayers != players.Count() || (players.Count() > 0 && players[0] != null && players[0].xPos < 0))
                {
                    localPlayer = null;
                    players = new Player[numberOfPlayers];
                    uint size = (uint)(numberOfPlayers * 0x04);
                    byte[] buffer = new byte[size];
                    pm.ReadMem(playerArray, size, out buffer);

                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        int player = BitConverter.ToInt32(buffer, i * 0x04);
                        if (player > 0)
                        {
                            Player p = new Player(player, pm);
                            p.loadPlayerData();
                            players[i] = p;

                        }
                    }
                }
                else
                {
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        if (players[i] != null)
                        {
                            players[i].loadPlayerData();
                        }
                    }
                }
            }

        }
    }
}
