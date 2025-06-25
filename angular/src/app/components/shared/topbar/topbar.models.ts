import { Button } from "../button/button.models";

export interface Topbar {
	id: string;
	title: string;
	mainButton: Button;
	secondaryButtons: Button[];
}
