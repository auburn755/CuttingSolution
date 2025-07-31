/*
    класс представляет собой "сервис состояния". Предназначен для хранения, передачи и уведомления об изменениях переменных ContentWidth и ContentHeight между компонентами
    public event Action? OnSizeChanged - событие на которое можно подписаться в других компонентах razor.
    
    ----------------------------------------

    это событие вызывается при записи нового значения в свойства ContentWidth или ContentHeight
    private set
            {
                if (_contentWidth != value) добавлена проверка что значение переменной меняется
                {
                    _contentWidth = value;
                    OnSizeChanged?.Invoke();
                }
            }
    -----------------------------------------  

    Метод UpdateSize вызывается из компонента-источника, чьи переменные нужно хранить и передавать в другие компоненты, например так: LayoutState.UpdateSize(width, height);
    
    public void UpdateSize(int width, int height)
        {
            ContentWidth = width;
            ContentHeight = height;
        }

    сервис нужно зарегерстировать в Program.cs
    builder.Services.AddScoped<LayoutStateService>();

    ------------------------------------------

    После подписки на событие, например: LayoutState.OnSizeChanged += StateHasChanged; в других компонентах, можно получать новые значения переменных
    например так: <MudText>Ширина контента: @LayoutState.ContentWidth px</MudText>, т.е. напрямую в элементе

    -------------------------------------------
    в компоненте нужно вставить @inject LayoutStateService LayoutState
 */

namespace CuttingVisualizer.Services
{
    public class LayoutStateService
    {
        private int _contentWidth;
        private int _contentHeight;

        public int ContentWidth
        {
            get => _contentWidth;
            private set
            {
                if (_contentWidth != value)
                {
                    _contentWidth = value;
                    OnSizeChanged?.Invoke();
                }
            }
        }

        public int ContentHeight
        {
            get => _contentHeight;
            private set
            {
                if (_contentHeight != value)
                {
                    _contentHeight = value;
                    OnSizeChanged?.Invoke();
                }
            }
        }

        public event Action? OnSizeChanged;

        public void UpdateSize(int width, int height)
        {
            ContentWidth = width;
            ContentHeight = height;
        }
    }
}
