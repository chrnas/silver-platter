import { useEffect, useState } from "react";
import "./css/Menu.css";
import type { MenuItem } from "../Types/MenuItem";
import { menuEntryService } from "../service/MenuEntryService";

/*
const sampleMenu: MenuCategory[] = [
  {
    id: "starters",
    title: "Starters",
    items: [
      { id: 1, name: "Charred Octopus", description: "Smoky paprika • Lemon aioli", allergy: "Gluten"},
      { id: 2, name: "Heirloom Tomato", description: "Burrata • Basil • Aged balsamic", price: "$10" },
    ],
  },
  {
    id: "mains",
    title: "Mains",
    items: [
      { id: 11, name: "Pan-Seared Salmon", description: "Herb butter • Seasonal veg", price: "$24", tag: "Chef" },
      { id: 12, name: "Prime Ribeye", description: "Truffle mash • Charred broccolini", price: "$32" },
    ],
  },
  {
    id: "desserts",
    title: "Desserts",
    items: [
      { id: 21, name: "Warm Chocolate Pudding", description: "Vanilla cream", price: "$8" },
      { id: 22, name: "Lemon Tart", description: "Citrus curd • Almond crust", price: "$9" },
    ],
  },
];*/


type MenuProps = {
  restaurantId: number;
};

const Menu: React.FC<MenuProps> = ({restaurantId}) => {
  const [items, setItems] = useState<MenuItem[]>([]);
  const [randomItem, setRandomItem] = useState<MenuItem>();

  useEffect(() => {
    if (!restaurantId) return;
    
    menuEntryService.getByRestaurantId(restaurantId)
      .then((data) => {
        let randomId = Math.floor(Math.random() * data.length);
        setRandomItem(data[randomId]);
        setItems(data);
      })

  }, [restaurantId]);



  return (
    <div className="menu-root">

      <main className="menu-main" role="main" aria-labelledby="menu-title">
        <h1 id="menu-title" className="menu-page-title">Our Menu</h1>

        <div className="menu-grid">
          

          <section className="menu-list">
            <ul className="items">
              {items.map((it) => (
                <li key={it.id} className="menu-item">
                  <div className="item-main">
                    <div className="item-title">
                      <span className="name">{it.name}</span>
                    </div>
                  </div>
                  {it.description && <p className="item-desc">{it.description}</p>}
                </li>
              ))}
            </ul>
              
            
          </section>

          <aside className="menu-side" aria-label="Featured and actions">
            <div className="featured">
              <h3>Tonight's Feature</h3>
              <p className="featured-dish">{randomItem?.name}</p>
              <p>{randomItem?.description}</p>
            </div>

            <div className="reserve-card">
              <h4>Book a table</h4>
              <p>Reserve your spot for dinner service.</p>
              <button className="btn-outline">Reserve</button>
            </div>
          </aside>
        </div>
      </main>
    </div>
  );
};

export default Menu;