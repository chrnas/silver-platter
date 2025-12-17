import { useNavigate } from "react-router-dom";

function PageButton(props: {name : string, ref : string}) {
    let nav = useNavigate();
    return (
        <div>
            <button type="button" onClick={() => nav(props.ref)}>{props.name}</button>
        </div>
    )
}

export default PageButton;
export namespace PageButton.JSX {};