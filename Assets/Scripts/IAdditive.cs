public interface IAdditive
{
    void Apply(TabletCircle circle);
}

public class FormedElementsAdditive : IAdditive
{
    private BloodSample _bloodSample;

    public FormedElementsAdditive(BloodSample bloodSample)
    {
        _bloodSample = bloodSample;
    }

    public void Apply(TabletCircle circle)
    {
        circle.AddFormedElements(_bloodSample);
    }
}

public class SerumAdditive : IAdditive
{
    private BloodSample _bloodSample;

    public SerumAdditive(BloodSample bloodSample)
    {
        _bloodSample = bloodSample;
    }

    public void Apply(TabletCircle circle)
    {
        circle.AddSerum(_bloodSample);
    }
}

public class ReagentAdditive : IAdditive
{
    private Reagent _reagent;

    public ReagentAdditive(Reagent reagent)
    {
        _reagent = reagent;
    }

    public void Apply(TabletCircle circle)
    {
        circle.AddReagent(_reagent);
    }
}