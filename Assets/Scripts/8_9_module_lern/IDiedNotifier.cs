using System;

public interface IDiedNotifier
{
    event Action Died;
}
