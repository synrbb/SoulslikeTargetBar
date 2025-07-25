#/bin/sh -ex

if [ -z "$1" -o ! -d "$1" ]; then
    echo "Usage: $0 PATH_TO_MODS"
    exit 2
fi

NAME=$(basename $(pwd))

dotnet build -c Release

[ -e "$1/$NAME" ] && rm -rf "$1/$NAME"

tar -cf- --transform="s,bin/.*/,,;s,^,$NAME/," \
    Config LICENSE ModInfo.xml "bin/Release/net481/$NAME.dll" \
    | tar -xvf- -C "$1"
