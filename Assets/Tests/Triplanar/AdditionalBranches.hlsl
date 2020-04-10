// ShaderGraph Function

void TextureAssetBranch_float( bool ipredicate, Texture2D itrue, Texture2D ifalse, out float unused, out Texture2D o)
{
	/*
	This is needed for the node to
	compile and fill the SurfaceDescription
	for the preview.
	*/
	unused = 0;

	o = ipredicate ? itrue : ifalse;
}