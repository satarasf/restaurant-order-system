import { it } from "node:test";

interface CartItem {
    menuItemId: number;
    name: string;
    price: number;
    quantity: number; 
}


class Cart{
    items = $state<CartItem[]>([]);
    
    get totalItems(){
        return this.items.reduce((acc, item) => acc + item.quantity, 0);
    }

    get totalPrice(){
        return this.items.reduce((acc, item) => acc + item.price * item.quantity, 0);
    }

    add(menuItemId: number, name: string, price: number){
        const existing = this.items.find((i) => i.menuItemId === menuItemId);

        if(existing){
            existing.quantity += 1;
        }else{
            this.items.push({ menuItemId, name, price, quantity: 1 });
        }
    }

    remove(menuItemId: number){
        const existing = this.items.find((i) => i.menuItemId === menuItemId);
        if(existing && existing.quantity > 1){
            existing.quantity -= 1;
        }else{
            this.items = this.items.filter((i) => i.menuItemId !== menuItemId)
        }
    }

    clear(){
        this.items = [];
    }
}

export const cart = new Cart();