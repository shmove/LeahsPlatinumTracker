﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeahsPlatinumTracker.Maps
{
    public partial class r203 : MapsForm
    {

        public r203()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            //MapSector Floaroma = Player.GetMapSector("Floaroma");
            //MapSector FloaromaPokecentre = Player.GetMapSector("Floaroma Pokecentre");

            // create warp buttons
            // CreateWarpButton(this, Floaroma.Warps[0], new Point(0, 0));

            CreateRouteConnectorButton(this, Player, "Jubilife", new Point(5, 285)); // connected to Jubilife on left

            MapImages.SendToBack();
        }

    }
}
