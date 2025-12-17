import PageButton from "../Components/PageButton";
import PageLogo from "../Components/PageLogo";
import './css/Header.css'

function Header() {
    return (
        <div className="Header">
            <PageLogo />
            <nav className="HeaderItems">
                <PageButton name="Home" ref="/" />
                <PageButton name="Browse" ref="/Browse" />
                <PageButton name="My Page" ref="/MyPage" />
            </nav>
        </div>
    )
}

export default Header;