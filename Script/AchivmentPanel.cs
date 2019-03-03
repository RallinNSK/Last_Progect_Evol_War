using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivmentPanel : MonoBehaviour {

    [Header("Scroll Content")]
    public ScrollRect scrollRect;
    [Header("Text field")]
    public Text achivNameField;
    public Text achivConditionField;
    [Header("Block field's")]
    public GameObject[] blocks;

    private void OnEnable()
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            if(PlayerPrefs.HasKey("Achivment_" + i))
            {
                blocks[i].SetActive(false);
            }
        }
        achivNameField.text = "";
        achivConditionField.text = "";
    }

    //Отображение текста ачивок
    public void AchivmentTextView(int index)
    {
        switch (index)
        {
            case 0:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Море мечей":"Sea of swords";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Нанять 1500 юнитов пехоты" : "Hire 1500 units footman";
                break;
            case 1:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Туча стрел" : "Cloud of arrows";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Нанять 1000 юнитов стрелков" : "Hire 1000 units rifleman";
                break;
            case 2:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Чёрное небо" : "The Dark sky";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Нанять 700 юнитов авиации" : "Hire 700 units aviation";
                break;
            case 3:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Армия непобедимых" : "The army invincible";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Нанять 500 супер юнитов" : "Hire 500 super units";
                break;
            case 4:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Ядерная бомба" : "Nuclear bomb";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Использовать апокалипсис 1 раз" : "Use the apocalypse 1 times";
                break;
            case 5:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Геноцид" : "Genocide";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Использовать апокалипсис 30 раз" : "Use the apocalypse 30 times";
                break;
            case 6:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Огневая мощь" : "Firepower";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Разблокировать и установить три вида башенных орудий" : "Unlock and install three types of tower cannon's";
                break;
            case 7:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Оружейник" : "Gunsmith";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Заменить / Продать башенные орудия 100 раз" : "Replace / Sell tower cannon's 100 times";
                break;
            case 8:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Дуэлянт" : "Duelist";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Победить в стандартном режиме игры" : "Win in standart game mode";
                break;
            case 9:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Захватчик" : "Invader";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Победить в режиме захват башни" : "Win in the capture tower mode";
                break;
            case 10:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Безумная битва" : "Insane Battle";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Победить в пользовательском режиме" : "Win in the capture tower mode";
                break;
            case 11:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Агрессивный захват" : "Aggressive capture";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Победить 100 раз" : "Win 100 times";
                break;
            case 12:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Капитан" : "Captain";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Победить в режиме сложности “Просто” 30 раз" : "Win in “Simple” difficulty mode 30 times";
                break;
            case 13:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Полковник" : "Colonel";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Победить в режиме сложности “Нормально” 20 раз" : "Win in “Normal” difficulty mode 20 times";
                break;
            case 14:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Маршал" : "Marshal";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Победить в режиме сложности “Сложно” 10 раз" : "Win in “Hard” difficulty mode 10 times";
                break;
            case 15:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Сеньор" : "Senior";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Улучшить 30 замков до 1 уровня" : "Upgrade 30 castle to level 1";
                break;
            case 16:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Феодал" : "Feudal";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Улучшить 40 замков до 2 уровня" : "Upgrade 40 castle to level 2";
                break;
            case 17:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Монарх" : "Monarch";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Улучшить 50 замков до 3 уровня" : "Upgrade 50 castle to level 3";
                break;
            case 18:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Сильнейший громила" : "The Strongest Thug";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Пережить этап эволюции “Каменный Век” 60 раз" : "Survive the stage of evolution of “Stone Age” 60 times";
                break;
            case 19:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Император поднебесной" : "The Emperor of Heaven";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Пережить этап эволюции “Китай” 50 раз" : "Survive the stage of evolution of “China” 50 times";
                break;
            case 20:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Король выбранный богом" : "The King chosen by God";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Пережить этап эволюции “Средние Века” 40 раз" : "Survive the stage of evolution of “Middle Age” 40 times";
                break;
            case 21:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Президент планеты" : "The President of the planet";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Пережить этап эволюции “Наши дни” 30 раз" : "Survive the stage of evolution of “Nowadays” 30 times";
                break;
            case 22:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Повелитель вселенной" : "The Master of the Universe";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Пережить этап эволюции “Будущие” 20 раз" : "Survive the stage of evolution of “Future” 20 times";
                break;
            case 23:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Божество из бездны" : "The Deity of the Abyss";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Пережить этап эволюции “Древние боги” 10 раз" : "Survive the stage of evolution of “Ancient god” 10 times";
                break;
            case 24:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Вершина эволюции" : "The pinnacle of evolution";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Пройти все этапы эволюции 15 раз" : "Complete all stages of evolution 15 times";
                break;
            case 25:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Гонец с вестями" : "A messenger with tidings";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Посмотреть рекламу за награду 3 раза" : "View reward ads 3 times";
                break;
            case 26:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Подать" : "Submit";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Получить 5000 монет за уровень" : "Get the 5000 coin for level";
                break;
            case 27:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Оброк" : "Servage";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Получить 10000 монет за уровень" : "Get the 10000 coin for level";
                break;
            case 28:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Великая дань" : "Great Tribute";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Получить 20000 монет за уровень" : "Get the 20000 coin for level";
                break;
            case 29:
                achivNameField.text = Application.systemLanguage == SystemLanguage.Russian ? "Мировые сокровища" : "World Treasures";
                achivConditionField.text = Application.systemLanguage == SystemLanguage.Russian ? "Получить 100000 монет за всё время" : "Get the 100000 coin all times";
                break;
        }
        MenuSettings.instance.SoundInterface(0);
    }

    //Закрытие окна
    public void CloseAchivmentPanel()
    {
        MenuSettings.instance.SoundInterface(0);
        gameObject.SetActive(false);
    }
    //Прокрутка Слайдера спомошью кнопок
    public void ContentSllideRight()
    {
        MenuSettings.instance.SoundInterface(0);
        scrollRect.content.position += Vector3.left * 3f;
    }
    public void ContentSllideLeft()
    {
        MenuSettings.instance.SoundInterface(0);
        scrollRect.content.position -= Vector3.left * 3f;
    }
}
