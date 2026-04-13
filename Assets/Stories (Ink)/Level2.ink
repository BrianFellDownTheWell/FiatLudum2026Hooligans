// Dragon Dialogue - Zenith
VAR ending = ""

Dragon: "FUCK YOU. Watch where you're flying jackass. You almost tore a hole in my wing!" #portrait:dragon

* [What the hell? You almost flew into me!]
    Dragon: "Oh, so you're a fucking liar. I kind of live in the sky cause I'm a fucking dragon, so I was here first." #portrait:dragon
    Dragon: "And since I was here first, that means you would have flown into me. If I wasn't here, there would have been no innocent dragon for you to injure. Sheesh." #portrait:dragon
    -> choice_two

* [I'm sorry…]
    Dragon: "YEAH YOU BETTER BE. Heh." #portrait:dragon
    -> choice_two


= choice_two

* [Do you hate me?]
    Dragon: "Huh, what? Why would I hate you? Dude, I was just telling you to be careful." #portrait:dragon
    -> choice_three

* [I should go…]
    Dragon: "Huh, what? Why? Dude, I was just telling you to be careful." #portrait:dragon
    -> choice_three


= choice_three

* [So yelling and belittling me is how you tell me to be careful?]
    Dragon: "No no no, I was just teasing. That's all." #portrait:dragon
    Dragon: "As your sibling, I gotta give you a hard time sometimes. Gotta prove I'm the better sibling, heh." #portrait:dragon
    Dragon: "..." #portrait:dragon
    Dragon: "I'm sorry, I love youuuu." #portrait:dragon
    -> choice_four

* [Fuck off.]
    Dragon: "Wait what? There's some misunderstanding here, as your sibling, I'm just trying to protect you from making costlier mistakes in the future, cause I love you!" #portrait:dragon
    Dragon: "And I gotta prove I'm the better sibling, heh." #portrait:dragon
    Dragon: "..." #portrait:dragon
    Dragon: "I'm sorry, I love youuuu." #portrait:dragon
    -> choice_four


= choice_four

* [How can I know you really love me?]
    Dragon: "You don't seriously think I don't love you, right? You actual dumbass." #portrait:dragon
    Dragon: "You know that if I didn't love you, I wouldn't be this comfortable teasing you. When I'm in class or around friends, I don't even have the courage to speak." #portrait:dragon
    Dragon: "I thought that because we were so close, you knew I didn't mean it when I was being mean." #portrait:dragon
    Dragon: "..." #portrait:dragon
    Dragon: "Fuck. Fuck you fuck you fuck you… FUCK YOU." #portrait:dragon
    Dragon: "..." #portrait:dragon
    Dragon: "Wait, not you, I mean, fuck me. Fuck you me. I'm so sorry I didn't realize how much my words hurt you sooner." #portrait:dragon
    Dragon: "..." #portrait:dragon
    Dragon: "Please don't leave. I'm sorry. If you do, I'll never be able to make it up to you." #portrait:dragon
    Dragon: "I can say I'm sorry, but I know that's not enough. Can you stay so I can earn your forgiveness?" #portrait:dragon
    -> choice_five

* [Sorry? After years of belittling me, that's all you can say?]
    Dragon: "The fuck? I haven't been belittling you. I'm just giving you that sibling love, that tough love you see in all the movies, heh." #portrait:dragon
    Dragon: "Like, if I didn't love you, you realize that I wouldn't be comfortable enough to tell you off when you're pissing me off. I'm such a people pleaser dude." #portrait:dragon
    Dragon: "I solo every group project I've been in and I've been in so many toxic relationships since I don't know how to set my boundaries." #portrait:dragon
    Dragon: "I knew you wouldn't leave if I express how I feel, so that's what I do. You're such a fucking dumbass." #portrait:dragon
    Dragon: "..." #portrait:dragon
    Dragon: "Oh, but I guess you are leaving now…" #portrait:dragon
    Dragon: "Fuck. Fuck you fuck you fuck you… FUCK YOU." #portrait:dragon
    Dragon: "Wait, not you, I mean, fuck me. Fuck you me. I'm so sorry." #portrait:dragon
    Dragon: "I couldn't even tell that you were hurting this much, and clearly, I'm hurting you, and I couldn't even tell." #portrait:dragon
    Dragon: "Please don't leave. I'm sorry. If you do, I'll never be able to make it up to you." #portrait:dragon
    Dragon: "I can say I'm sorry, but I know that's not enough. Can you stay so I can earn your forgiveness?" #portrait:dragon
    -> choice_five


= choice_five

* [I'll give you a second chance.]
    Dragon: "Really? You're the best, I don't deserve you." #portrait:dragon
    Dragon: "You know how you always wanted to see the 7 Wonders of the World? I've been wanting to see them too…" #portrait:dragon
    Dragon: "Let's go see them together! It'll be a fresh start for us. Heh." #portrait:dragon
    Dragon: "You're like really high though, do you need me to pop your balloon so you can get down?" #portrait:dragon

    * * [Pop it.]
        -> end_pop
    * * [Later.]
        -> end_later

* [No, you already broke me once.]
    Dragon: "Oh. Yeah, I probably wouldn't forgive myself either, heh." #portrait:dragon
    Dragon: "Welp, take care. If you need me, I'll be there. Even if I'm ass at showing it, I love you." #portrait:dragon
    Dragon: "Heh. Fuck me." #portrait:dragon
    -> end_later


= end_pop
~ ending = "pop"
// Pop it ending
-> END

= end_later
~ ending = "later"
// Later ending
-> END