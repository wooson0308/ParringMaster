using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Warriors.Main
{
    public enum MenuType
    {
        Intro,
        Main,
        Setting,
        Game
    }

    [Serializable]
    public class Menu 
    { 
        public MenuType type;
        public RectTransform parent;

        private bool onComplete = true;
        public bool OnComplete => onComplete;

        public void SetActive(bool active)
        {
            Vector3 scale = active ? Vector3.one : Vector3.zero;
            Ease ease = active ? Ease.OutBack : Ease.InBack;

            parent.DOScale(scale, .5f)
                .SetEase(ease)
                .OnStart(()=> {
                    onComplete = false;
                })
                .OnComplete(() => {
                    onComplete = true;
                });
        }
    }

    [Serializable]
    public class MenuButton 
    { 
        public MenuType type; 
        [SerializeField] private Button button;

        public void AddListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }
    }

    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<Menu> menuList;
        [SerializeField] private List<MenuButton> menuButtonList;

        private Menu currentMenu;

        private Menu GetMenu(MenuType type)
        {
            Menu returnMenu = null;

            foreach (Menu menu in menuList)
            {
                if (menu.type.Equals(type))
                {
                    returnMenu = menu;
                    break;
                }
            }

            return returnMenu;
        }

        private void OnEnable()
        {
            currentMenu = GetMenu(MenuType.Intro);

            foreach (MenuButton menubutton in menuButtonList)
            {
                menubutton.AddListener(() => OpenMenu(menubutton.type));
            }
        }

        private void OpenMenu(MenuType menuType)
        {
            StartCoroutine(CoOpenMenu(menuType));
        }

        private IEnumerator CoOpenMenu(MenuType menuType)
        {
            yield return StartCoroutine(CoWaitMenuTweens());

            currentMenu = GetMenu(menuType);
            currentMenu.SetActive(true);
        }

        private IEnumerator CoWaitMenuTweens()
        {
            if (currentMenu == null) yield break;

            CloseMenu();

            yield return null;
            yield return new WaitUntil(() => currentMenu.OnComplete);
        }


        private void CloseMenu()
        {
            currentMenu.SetActive(false);
        }
    }
}


