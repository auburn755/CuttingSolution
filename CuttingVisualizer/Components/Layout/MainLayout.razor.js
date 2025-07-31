let observer;
let lastWidth = 0;
let lastHeight = 0;
let timeoutId = null;
const DEBOUNCE_DELAY = 100; // Задержка в миллисекундах

export function initResizeObserver(element, dotNetHelper) {
    observer = new ResizeObserver(entries => {
        if (!entries || entries.length === 0) return;

        const entry = entries[0];
        const newWidth = Math.round(entry.contentRect.width);
        const newHeight = Math.round(entry.contentRect.height);

        // Если размеры не изменились - игнорируем
        if (newWidth === lastWidth && newHeight === lastHeight) return;

        // Сбрасываем предыдущий таймер, если он был
        if (timeoutId) {
            clearTimeout(timeoutId);
        }

        // Устанавливаем новый таймер
        timeoutId = setTimeout(() => {
            lastWidth = newWidth;
            lastHeight = newHeight;
            dotNetHelper.invokeMethodAsync('UpdateContentSize', newWidth, newHeight);  //вызов обработчика события в объекте CuttingViewer. Он должен быть определен как:
            // [JSInvokable]
            // public void UpdateContentSize(int width, int height) {...}
        }, DEBOUNCE_DELAY);
    });

    observer.observe(element);
}

export function disposeResizeObserver() {
    if (timeoutId) {
        clearTimeout(timeoutId);
    }
    if (observer) {
        observer.disconnect();
    }
}