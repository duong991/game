using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instruction 
{
    public virtual void Excute(GameSystem system) {  }
    
    public virtual IEnumerator Animation(GameSystem system) { yield return null; }
}

public class Inbox : Instruction
{
    bool destroy = false;

    public override void Excute(GameSystem system)
    {
        
        system.SetCurrent(system.Dequeueinbox());

    }


    public override IEnumerator Animation(GameSystem system)
    {



        while(!system.Player.MoveToInput() )
        {
            
            yield return null;
        }
      
        system.DrawSystem.UpdateInbox();
        if(system.GetCurrent().Value != -1 )
        {
        system.Player.DisplayCurrent(system.GetCurrent().Value.ToString());
        }
        system.Player.SetStepcong();

    }
    public override string ToString()
    {
        return "INBOX ";
    }
}
public class Outbox : Instruction
{
    public override void Excute(GameSystem system)
    {
        try
        {
            system.SendToOutbox(system.GetCurrent().Value);
            system.SetCurrent(null);
        }

        catch(Exception ex)
        {

        }

    }

    public override IEnumerator Animation(GameSystem system)
    {
        //while (system.Player.DestroyCurrent())
            //yield return null;

        while (!system.Player.MoveToOutput())
        {
            yield return null;
        }
        system.DrawSystem.UpdateOutbox();
        system.Player.HideCurrent();
        system.Player.SetStepcong();
    }
    public override string ToString()
    {
        return "OUTBOX ";
    }
}


public class Copyto : Instruction
{
    int index;

    public Copyto(int index)
    {
        this.index = index;
    }
        
    public override void Excute(GameSystem system)
    {
        system.CopytoMemory(index, system.GetCurrent().Value);

        
 
    }

    public override IEnumerator Animation(GameSystem system)
    {
        while(!system.Player.MovetoMemory(index))
        {
            yield return null;
        }
        system.DrawSystem.UpdateMemory(system.GetMemory());
        //Debug.Log(system.GetMemory());
        system.Player.SetStepcong();
    }
    public override string ToString()
    {
        return "COPYTO "+index;
    }
}

public class CopyFrom : Instruction
{
    int index;
    bool adress;

    public CopyFrom(int index, bool adress)
    {
        this.index = index;
        this.adress = adress;
    }

   

    public override void Excute(GameSystem system)
    {
        if (adress)
        {
            //Debug.Log(index);
            system.SetCurrent(system.GetMemory()[system.GetMemory()[index]]);
        }
        else
            system.SetCurrent(system.GetMemory()[index]);

    }

    public override IEnumerator Animation(GameSystem system)
    {
        if(adress)
        {
            while (!system.Player.MovetoMemory(system.GetMemory()[index]))
            {
                yield return null;
            }
        }
        else
        {
            while (!system.Player.MovetoMemory(index))
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.5f);
        system.Player.DisplayCurrent(system.GetCurrent().Value.ToString());
        system.Player.SetStepcong();
    }
    public override string ToString()
    {
        if (adress)
            return "COPYFROM [" + index + "]";
        else
            return "COPYFROM " + index;
    }

}

public class Add : Instruction
{
    int index;
    public Add(int index)
    {
        this.index = index;
    }
    string addition = "";

    public override void Excute(GameSystem system)
    {
        addition = system.GetCurrent().Value.ToString() + " + " + system.GetMemory()[index].ToString();
        //Debug.Log(addition);

        system.SetCurrent(system.GetMemory()[index] + system.GetCurrent().Value);

    }
    public override IEnumerator Animation(GameSystem system)
    {

        while (!system.Player.MovetoMemory(index))
        {
            yield return null;

        }


        system.Player.DisplayCurrent(addition);
        yield return new WaitForSeconds(1f);
        system.Player.DisplayCurrent(system.GetCurrent().Value.ToString());
        system.Player.SetStepcong();
    }
    public override string ToString()
    {
        return "ADD " + index;
    }
}

public class Sub : Instruction
{
    int index;
    public Sub(int index)
    {
        this.index = index;
    }
    string subition = "";

    public override void Excute(GameSystem system)
    {


        subition = system.GetCurrent().Value.ToString() + " - " + system.GetMemory()[index].ToString();
        //Debug.Log(addition);

        system.SetCurrent(system.GetCurrent().Value - system.GetMemory()[index]  );

    }
    public override IEnumerator Animation(GameSystem system)
    {
        while (!system.Player.MovetoMemory(index))
        {
            yield return null;

        }
        system.Player.DisplayCurrent(subition);
        yield return new WaitForSeconds(1f);
        system.Player.DisplayCurrent(system.GetCurrent().Value.ToString());
        system.Player.SetStepcong();
    }
    public override string ToString()
    {
        return "SUB " + index;
    }
}

public class Jump : Instruction
{
    string label;
    public Jump(string label)
    {
        this.label = label;
    }

    public override void Excute(GameSystem system)
    {
        system.JumpToLabel(label);
      
    }
  

    public override string ToString()
    {
        return "JUMP " + label;
    }
}

public class JumpZ : Instruction
{
    string label;
    public JumpZ(string label)
    {
        this.label = label;
    }

    public override void Excute(GameSystem system)
    {
        //Debug.Log(system.GetCurrent().Value);

        if(system.GetCurrent().Value == 0)
        {
            system.JumpToLabel(label);
        }
        
        
            

    }
   

    public override string ToString()
    {
        return "JUMPZ " + label;
    }

}

public class DoNothing : Instruction
{
    public override string ToString()
    {
        return "DONOTHING ";
    }
}

