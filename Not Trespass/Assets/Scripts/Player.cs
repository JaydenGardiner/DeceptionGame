using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Player
{
    public string Name { get; private set; }

    public Guid ID { get; private set; }

    public Player(string pName)
    {
        this.Name = pName;
        ID = Guid.NewGuid();
    }
}

