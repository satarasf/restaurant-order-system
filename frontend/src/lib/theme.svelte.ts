'use strict';

function getInitialTheme(): 'light' | 'dark' {
    if(typeof localStorage !== 'undefined'){
        const gespeicherteTheme = localStorage.getItem('theme');

        if(gespeicherteTheme === 'light' || gespeicherteTheme === 'dark'){
            return gespeicherteTheme;
        }
    }
    
    return 'dark';
}

class ThemeStore{
    current = $state<'light' | 'dark'>(getInitialTheme());
    
    toggle(){
        this.current = this.current === 'dark' ? 'light' : 'dark';

        document.documentElement.setAttribute('data-theme', this.current);

        //Die Wahl wird dauerhaft gespeichert, damit es beim nächsten Besuch wieder automatisch geladen wird.
        localStorage.setItem('theme', this.current);
    }

    apply(){
        document.documentElement.setAttribute('data-theme', this.current);
    }
}

export const theme = new ThemeStore();