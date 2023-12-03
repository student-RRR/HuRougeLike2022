using System.Collections.Generic;

public abstract class GameSystem : IGameSystem
{
    protected HuRougeLikeGame gameMediator = null;
    public GameSystem(HuRougeLikeGame _mediator)
    {
        gameMediator = _mediator;
    }

    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}
