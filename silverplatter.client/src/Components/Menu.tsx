import { useEffect, useState } from "react";
import "./css/Menu.css";
import type { MenuItem } from "../Types/MenuItem";
import { menuEntryService } from "../service/MenuEntryService";

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
          </aside>
        </div>
      </main>
    </div>
  );
};

export default Menu;