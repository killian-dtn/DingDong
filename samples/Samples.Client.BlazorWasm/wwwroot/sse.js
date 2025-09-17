window.createEventSourceWrapper = (url) => {
    const wrapper = class
    {
        constructor(url)
        {
            this.es = new EventSource(url);
        }

        addListener(key, dotnetMethodInstanceObject, methodName)
        {
            this.es.addEventListener(key, event => {
                dotnetMethodInstanceObject.invokeMethodAsync(methodName, event.data);
            });
        }

        close()
        {
            this.es.close();
        }
    };

    return new wrapper(url);
};
