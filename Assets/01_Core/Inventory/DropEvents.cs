using System;

public static class DropEvents
{
    public static event Action<SlotView, SlotView> OnSlotInteractionAttempt;

    public static void NotifyInteraction(SlotView source, SlotView destination)
    {
        OnSlotInteractionAttempt?.Invoke(source,destination);
    }
}
