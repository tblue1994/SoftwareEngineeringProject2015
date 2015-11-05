using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

public class MyApiResolver : System.Web.Http.Dependencies.IDependencyResolver
{
    protected IUnityContainer container;

    public MyApiResolver(IUnityContainer container)
    {
        if (container == null)
        {
            throw new ArgumentNullException("container");
        }
        this.container = container;
    }

    public object GetService(Type serviceType)
    {
        try
        {
            return container.Resolve(serviceType);
        }
        catch (ResolutionFailedException e)
        {
            return null;
        }
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        try
        {
            return container.ResolveAll(serviceType);
        }
        catch (ResolutionFailedException)
        {
            return new List<object>();
        }
    }

    public System.Web.Http.Dependencies.IDependencyScope BeginScope()
    {
        var child = container.CreateChildContainer();
        return new MyApiResolver(child);
    }

    public void Dispose()
    {
        container.Dispose();
    }
}
public class MvcResolver : System.Web.Mvc.IDependencyResolver
{
    protected IUnityContainer container;

    public MvcResolver(IUnityContainer container)
    {
        if (container == null)
        {
            throw new ArgumentNullException("container");
        }
        this.container = container;
    }

    public object GetService(Type serviceType)
    {
        try
        {
            return container.Resolve(serviceType);
        }
        catch (ResolutionFailedException)
        {
            return null;
        }
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        try
        {
            return container.ResolveAll(serviceType);
        }
        catch (ResolutionFailedException)
        {
            return new List<object>();
        }
    }

    public void Dispose()
    {
        container.Dispose();
    }
}