using System;
using System.Collections.ObjectModel;

namespace Default_MVVM {
    public class TestDataViewModel {
        public ObservableCollection<TestObjectViewModel> Records { get; set; }
        public TestDataViewModel() {
            Records = new ObservableCollection<TestObjectViewModel>();            
            for (int i = 0; i < 200; i++)
                Records.Add(new TestObjectViewModel() {
                    Text = "Row" + i,
                    Number = i,
                    Date = DateTime.Now.AddDays(i).AddHours(i)
                });
        }        
    }

    public class TestObjectViewModel {
        public string Text { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
    }
}
