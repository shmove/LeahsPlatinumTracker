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
    public partial class r226 : MapsForm
    {

        public r226()
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

            CreateRouteConnectorButton(this, Player, "SurvivalArea", new Point(5, 283)); // path left to survival area
            CreateRouteConnectorButton(this, Player, "227", new Point(702, 60)); // path top right to 227

            MapImages.SendToBack();
        }

    }
}
